let dotNetRef;
let recognizer;
let sdkLoadPromise = null;

export async function init(elem, dotNetReference) {
    dotNetRef = dotNetReference;

    elem.focus();

    elem.addEventListener('input', () => resizeToFit(elem));
    afterPropertyWritten(elem, 'value', () => resizeToFit(elem));

    elem.addEventListener('keydown', (e) => {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            elem.dispatchEvent(new CustomEvent('change', { bubbles: true }));
            elem.closest('form').dispatchEvent(new CustomEvent('submit', { bubbles: true, cancelable: true }));
        }
    });

    return await checkSpeechAvailable();
}

async function checkSpeechAvailable() {
    if (!navigator.mediaDevices?.getUserMedia) return false;

    try {
        const res = await fetch('/api/speech/available');
        if (!res.ok) return false;
        return await res.json() === true;
    } catch {
        return false;
    }
}

export async function startListening() {
    const sdk = await loadSpeechSDK();

    const res = await fetch('/api/speech/token');
    if (!res.ok) throw new Error(`Failed to fetch speech token (HTTP ${res.status})`);

    const { token, region } = await res.json();

    const speechConfig = sdk.SpeechConfig.fromAuthorizationToken(token, region);
    speechConfig.speechRecognitionLanguage = getBcp47Language();

    const audioConfig = sdk.AudioConfig.fromDefaultMicrophoneInput();
    recognizer = new sdk.SpeechRecognizer(speechConfig, audioConfig);

    recognizer.recognized = (_s, e) => {
        if (e.result.reason === sdk.ResultReason.RecognizedSpeech && e.result.text) {
            dotNetRef.invokeMethodAsync('OnSpeechRecognized', e.result.text);
        }
    };

    recognizer.sessionStopped = () => {
        dotNetRef.invokeMethodAsync('OnSpeechStopped');
        recognizer = null;
    };

    recognizer.canceled = (_s, e) => {
        if (e.reason === sdk.CancellationReason.Error) {
            console.error('[Speech] Recognition canceled with error:', e.errorDetails);
        }
        dotNetRef.invokeMethodAsync('OnSpeechStopped');
        recognizer = null;
    };

    await new Promise((resolve, reject) => {
        recognizer.startContinuousRecognitionAsync(resolve, reject);
    });
}

export function stopListening() {
    if (recognizer) {
        const r = recognizer;
        recognizer = null;
        r.stopContinuousRecognitionAsync();
    }
}

function getBcp47Language() {
    const lang = navigator.language || 'en-US';
    return lang.includes('-') ? lang : 'en-US';
}

function loadSpeechSDK() {
    if (sdkLoadPromise) return sdkLoadPromise;

    sdkLoadPromise = new Promise((resolve, reject) => {
        if (window.SpeechSDK) {
            resolve(window.SpeechSDK);
            return;
        }

        const script = document.createElement('script');
        script.src = '/lib/speech/microsoft.cognitiveservices.speech.sdk.bundle-min.js';
        script.onload = () => {
            if (window.SpeechSDK) {
                resolve(window.SpeechSDK);
            } else {
                sdkLoadPromise = null;
                reject(new Error('Speech SDK script loaded but window.SpeechSDK is not defined'));
            }
        };
        script.onerror = () => {
            sdkLoadPromise = null;
            reject(new Error('Failed to load Azure Speech SDK from /lib/speech/'));
        };
        document.head.appendChild(script);
    });

    return sdkLoadPromise;
}

function resizeToFit(elem) {
    const lineHeight = parseFloat(getComputedStyle(elem).lineHeight);
    elem.rows = 1;
    const numLines = Math.ceil(elem.scrollHeight / lineHeight);
    elem.rows = Math.min(5, Math.max(1, numLines));
}

function afterPropertyWritten(target, propName, callback) {
    const descriptor = getPropertyDescriptor(target, propName);
    Object.defineProperty(target, propName, {
        get: function () {
            return descriptor.get.apply(this, arguments);
        },
        set: function () {
            const result = descriptor.set.apply(this, arguments);
            callback();
            return result;
        }
    });
}

function getPropertyDescriptor(target, propertyName) {
    return Object.getOwnPropertyDescriptor(target, propertyName)
        || getPropertyDescriptor(Object.getPrototypeOf(target), propertyName);
}
