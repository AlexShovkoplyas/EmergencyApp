const fs = require('fs');
const path = require('path');

const src = path.resolve(__dirname, '../node_modules/microsoft-cognitiveservices-speech-sdk/distrib/browser/microsoft.cognitiveservices.speech.sdk.bundle-min.js');
const destDir = path.resolve(__dirname, '../wwwroot/lib/speech');
const dest = path.join(destDir, 'microsoft.cognitiveservices.speech.sdk.bundle-min.js');

if (!fs.existsSync(destDir)) {
    fs.mkdirSync(destDir, { recursive: true });
}

fs.copyFileSync(src, dest);
console.log(`Copied Speech SDK → ${dest}`);
