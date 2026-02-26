using EmergencyApp.SheltersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergencyApp.SheltersApi.Data;

public static class ShelterSeeder
{
    public static async Task SeedAsync(SheltersDbContext db)
    {
        await db.Database.EnsureCreatedAsync();

        if (await db.BombShelters.AnyAsync())
            return;

        db.BombShelters.AddRange(GetShelters());
        await db.SaveChangesAsync();
    }

    private static IEnumerable<BombShelter> GetShelters() =>
    [
        new()
        {
            Name = "Schronisko Rynek Główny",
            Address = "Rynek Główny 1, Kraków",
            Latitude = 50.0617, Longitude = 19.9373,
            Capacity = 250,
            Description = "Duże schronisko w podziemiach historycznej kamienicy przy Rynku Głównym. Posiada pełne wyposażenie awaryjne: agregat prądotwórczy, zbiorniki na wodę pitną (zapas na 72h), ogrzewanie gazowe z redundancją elektryczną oraz zapasy żywnościowe — suche racje na 3 doby dla 250 osób. Obiekt regularnie inspekcjonowany przez Urząd Miasta. Wejście od strony ul. Szewskiej, przystosowane dla osób niepełnosprawnych."
        },
        new()
        {
            Name = "Schron Wawel – Parking Podziemny",
            Address = "ul. Bernardyńska 5, Kraków",
            Latitude = 50.0540, Longitude = 19.9357,
            Capacity = 400,
            Description = "Underground parking facility beneath the Wawel hill area repurposed as emergency shelter. Reinforced concrete structure with 3m ceiling height. Power backup via diesel generator. Running water available. No permanent heating — portable heaters are stored on site and can be deployed. Food supplies are not stocked here; visitors are advised to bring their own provisions."
        },
        new()
        {
            Name = "Schronisko Kazimierz – Piwnice Ul. Szeroka",
            Address = "ul. Szeroka 20, Kraków",
            Latitude = 50.0504, Longitude = 19.9472,
            Capacity = 120,
            Description = "Historyczne piwnice w dzielnicy Kazimierz. Dostępna elektryczność z agregatu oraz ogrzewanie elektryczne. Zamiast bieżącej wody dostępne są beczki z zapasami wody pitnej. Brak zapasów żywności."
        },
        new()
        {
            Name = "Schron Podgórze – Centrum Kultury",
            Address = "ul. Limanowskiego 15, Kraków",
            Latitude = 50.0430, Longitude = 19.9560,
            Capacity = 180,
            Description = "Shelter located beneath the Podgórze Cultural Center. The basement spans two levels with separate sections for families and single individuals. Electricity, running water, and heating are all available. Emergency food rations are kept in storage — enough for the full 180-person capacity for three days. Medical first aid kit present. Emergency radio communication equipment installed. Regular drills conducted quarterly."
        },
        new()
        {
            Name = "Schron AGH – Budynek A0",
            Address = "al. Mickiewicza 30, Kraków",
            Latitude = 50.0647, Longitude = 19.9234,
            Capacity = 350,
            Description = "Schronisko pod budynkiem głównym AGH. Elektryczność z sieci uczelnianej i agregatu, bieżąca woda, ogrzewanie centralne. Brak zapasów żywności — studenci i pracownicy zachęcani do przyniesienia własnych."
        },
        new()
        {
            Name = "Schron Uniwersytet Jagielloński – Collegium Novum",
            Address = "ul. Gołębia 24, Kraków",
            Latitude = 50.0607, Longitude = 19.9307,
            Capacity = 200,
            Description = "Basement emergency shelter under Collegium Novum. Accessed through a reinforced door on the north side of the building. Electricity is supplied by a UPS system with 8-hour backup. Water tanks hold approximately 2,000 litres. No heating available — the stone walls maintain a relatively stable temperature of around 12°C year-round. Not recommended for extended stays in winter without personal thermal equipment. No food stored on site."
        },
        new()
        {
            Name = "Schron Bronowice – Centrum Handlowe",
            Address = "ul. Głogowska 1, Kraków",
            Latitude = 50.0774, Longitude = 19.8900,
            Capacity = 600,
            Description = "Największe schronisko w zachodniej części Krakowa, zlokalizowane w podziemiach centrum handlowego. Pojemność 600 osób. Prąd z dwóch agregatów rezerwowych, woda z sieci miejskiej i zbiorników awaryjnych (10 000 L), ogrzewanie pompą ciepła i grzejnikami elektrycznymi, żywność w postaci racji na 5 dni dla wszystkich. Toalety, natryski, strefa dla dzieci. Personel dyżurny 24/7 w czasie alertu. Wjazd od strony parkingu podziemnego P2."
        },
        new()
        {
            Name = "Schron Nowa Huta – Aleja Różana",
            Address = "al. Różana 12, Nowa Huta, Kraków",
            Latitude = 50.0693, Longitude = 20.0500,
            Capacity = 300,
            Description = "Soviet-era civil defense shelter in Nowa Huta district, originally built in 1956. Recently renovated with modern utilities. Fully equipped: dual generator for electricity, a water filtration system providing clean drinking water, forced-air heating, and a 7-day food stockpile. Air filtration system installed. Blast doors rated for 2 bar overpressure. Staff trained in civil defense procedures."
        },
        new()
        {
            Name = "Schronisko Krowodrza – Szkoła Podstawowa nr 14",
            Address = "ul. Królewska 86, Kraków",
            Latitude = 50.0720, Longitude = 19.9220,
            Capacity = 150,
            Description = "Piwnica szkolna. Woda z sieci miejskiej dostępna. Brak własnego agregatu i ogrzewania."
        },
        new()
        {
            Name = "Schron Łagiewniki – Sanktuarium",
            Address = "ul. Siostry Faustyny 3, Kraków",
            Latitude = 50.0228, Longitude = 19.9367,
            Capacity = 500,
            Description = "Emergency shelter located in the underground infrastructure of the Divine Mercy Sanctuary complex. Large capacity due to the extensive basement network. Electricity from the campus utility system, water available, and heating maintained by the campus boiler. Food reserves — maintained by the monastic community — are sufficient for 3 to 4 days. First aid station with a volunteer nurse on-call during high-alert periods. Signage in Polish, English, and Ukrainian."
        },
        new()
        {
            Name = "Schron Czyżyny – Dawne Lotnisko",
            Address = "ul. Jana Pawła II 37, Kraków",
            Latitude = 50.0720, Longitude = 20.0040,
            Capacity = 220,
            Description = "Schron techniczny na terenie dawnego lotniska wojskowego. Prąd zapewniany przez agregat przemysłowy. Brak bieżącej wody i ogrzewania — obiekt przeznaczony głównie do krótkotrwałego schronienia."
        },
        new()
        {
            Name = "Schron Dębniki – Urząd Dzielnicy",
            Address = "ul. Rynek Dębnicki 6, Kraków",
            Latitude = 50.0447, Longitude = 19.9250,
            Capacity = 130,
            Description = "Shelter beneath the Dębniki district office. Electricity from city grid with backup generator, municipal water connection, and heating via the building's central system. No dedicated food storage on-site — the nearest emergency food distribution point is 400 m away at the fire station. Accessible for people with disabilities. Dogs allowed in a designated area."
        },
        new()
        {
            Name = "Schronisko Stare Miasto – Teatr Słowackiego",
            Address = "pl. Świętego Ducha 1, Kraków",
            Latitude = 50.0651, Longitude = 19.9424,
            Capacity = 80,
            Description = "Piwnice teatralne przystosowane jako schron. Elektryczność z systemu UPS teatru, bieżąca woda. Brak ogrzewania. Pojemność 80 osób."
        },
        new()
        {
            Name = "Schron Mistrzejowice – Blok Komunalny",
            Address = "os. Tysiąclecia 75, Kraków",
            Latitude = 50.0910, Longitude = 20.0000,
            Capacity = 160,
            Description = "Schronisko w piwnicy bloku mieszkalnego, zmodernizowane w 2021 roku. Ogrzewanie elektryczne, prąd z miejskiej sieci i małego agregatu awaryjnego, woda z wodociągów. Miejsca siedzące dla 160 osób, w tym 20 miejsc leżących. Apteczka pierwszej pomocy. Mieszkańcy proszeni o przyniesienie własnych zapasów żywności."
        },
        new()
        {
            Name = "Schron Prądnik Czerwony – Osiedle",
            Address = "ul. Fieldorfa 10, Kraków",
            Latitude = 50.0850, Longitude = 19.9640,
            Capacity = 200,
            Description = "Community shelter in the Prądnik Czerwony district, built in the late 1970s. Solid reinforced concrete construction. Power and water available. No heating installed — blankets are stored on site for approximately 50 people. The listed capacity is 200 but the space is realistically comfortable for around 150."
        },
        new()
        {
            Name = "Schron Swoszowice – Balneologiczny",
            Address = "ul. Uzdrowiskowa 5, Swoszowice, Kraków",
            Latitude = 49.9940, Longitude = 19.9230,
            Capacity = 90,
            Description = "Małe schronisko przy uzdrowisku w Swoszowicach. Elektryczność, bieżąca woda, ogrzewanie i zapasy żywności na 3 doby. Dostępna apteczka i personel medyczny uzdrowiska."
        },
        new()
        {
            Name = "Schron Bieżanów – Hala Sportowa",
            Address = "ul. Bieżanowska 50, Kraków",
            Latitude = 50.0180, Longitude = 20.0200,
            Capacity = 280,
            Description = "Underground shelter beneath the Bieżanów sports hall, converted during a 2019 renovation. Heating via the hall's heat pump system. Water and electricity from city utilities. Emergency lighting throughout. Well-ventilated with a mechanical air exchange system. No food stored on-site — local emergency management recommends bringing a personal 48-hour supply."
        },
        new()
        {
            Name = "Schron Prokocim – Szpital",
            Address = "ul. Wielicka 265, Kraków",
            Latitude = 50.0150, Longitude = 20.0050,
            Capacity = 350,
            Description = "Szpitalny schron z priorytetem dla pacjentów i personelu. Pełne wyposażenie medyczne, prąd z agregatów szpitalnych, woda, ogrzewanie, żywność z kuchni szpitalnej. Ogólnodostępny wyłącznie w przypadku przepełnienia innych schronisk."
        },
        new()
        {
            Name = "Schron Zwierzyniec – Wille",
            Address = "ul. Królowej Jadwigi 40, Kraków",
            Latitude = 50.0570, Longitude = 19.9040,
            Capacity = 60,
            Description = "Small private-turned-public shelter in the Zwierzyniec villa district. Damp stone basement. Water available via a hand pump connected to an old well. No electricity or heating. Suitable for short-term shelter only — capacity strictly limited to 60 people."
        },
        new()
        {
            Name = "Schron Ruczaj – Kampus UJ",
            Address = "ul. Łojasiewicza 4, Kraków",
            Latitude = 50.0270, Longitude = 19.9070,
            Capacity = 400,
            Description = "Modern emergency shelter under the Jagiellonian University Ruczaj campus, built to contemporary civil defense standards. 72-hour battery and generator power, pressurized water tanks, underfloor heating, and catered emergency meal packs for 400 people for 72 hours. Decontamination showers available. Separate accessible zone. University security manages the facility and it is open to the general public during alerts."
        },
        new()
        {
            Name = "Schronisko Wola Duchacka",
            Address = "ul. Kostaneckiego 1, Kraków",
            Latitude = 50.0090, Longitude = 19.9600,
            Capacity = 110,
            Description = "Schron osiedlowy. Zasilanie z agregatu. Brak wody i ogrzewania."
        },
        new()
        {
            Name = "Schron Zabłocie – Fabryka Schindlera",
            Address = "ul. Lipowa 4, Kraków",
            Latitude = 50.0480, Longitude = 19.9660,
            Capacity = 140,
            Description = "Historic industrial basement beneath the former Schindler factory. Electricity from museum backup systems and running water available. No heating — the thick brick walls provide natural thermal mass that keeps temperatures stable. Access through the museum rear entrance. Historical information boards are present even within the shelter area."
        },
        new()
        {
            Name = "Schron Grzegórzki – Szkoła Muzyczna",
            Address = "ul. Basztowa 8, Kraków",
            Latitude = 50.0660, Longitude = 19.9490,
            Capacity = 95,
            Description = "Ogrzewana piwnica szkoły muzycznej. Prąd i woda dostępne. Brak zapasów żywności. Miejsce dla 95 osób."
        },
        new()
        {
            Name = "Schron Kurdwanów – Blok nr 12",
            Address = "os. Kurdwanów 12, Kraków",
            Latitude = 50.0050, Longitude = 19.9440,
            Capacity = 175,
            Description = "Residential block shelter on Kurdwanów estate managed by the local housing cooperative. Regularly restocked with emergency food: canned goods, water sachets, energy bars. Heating via electric radiators. Good lighting, basic first aid kit, emergency radio. Capacity is 175 but expect close quarters — priority for elderly residents of the block, then general public."
        },
        new()
        {
            Name = "Schron Wzgórza Krzesławickie",
            Address = "ul. Górka Narodowa 1, Kraków",
            Latitude = 50.1050, Longitude = 20.0100,
            Capacity = 70,
            Description = "Schron górski — wyłącznie budowla. Brak jakichkolwiek mediów ani zapasów. Zapewnia jedynie ochronę przed odłamkami i falą uderzeniową."
        },
        new()
        {
            Name = "Schron Krowodrzańska – Biblioteka",
            Address = "ul. Krowoderska 24, Kraków",
            Latitude = 50.0700, Longitude = 19.9310,
            Capacity = 120,
            Description = "Library basement converted to shelter. Power from the library's backup system, water available. No permanent heating but electric heaters can be deployed from storage. Quiet environment suitable for families with children. Emergency information pamphlets available in multiple languages. No food stocked."
        },
        new()
        {
            Name = "Schron Mydlniki – Remiza OSP",
            Address = "ul. Mydlnicka 30, Kraków",
            Latitude = 50.0750, Longitude = 19.8680,
            Capacity = 80,
            Description = "Schronisko przy remizie ochotniczej straży pożarnej w Mydlnikach. Straż prowadzi dyżury i zarządza obiektem. Prąd z agregatu strażackiego, bieżąca woda, ogrzewanie oraz suche racje żywnościowe na 48 godzin. Apteczka rozszerzona. Komunikacja radiowa z centrum zarządzania kryzysowego."
        },
        new()
        {
            Name = "Schron Bieńczyce – Centrum Handlowe Nowa Huta",
            Address = "al. Jana Pawła II 180, Kraków",
            Latitude = 50.0820, Longitude = 20.0300,
            Capacity = 450,
            Description = "Large commercial basement in eastern Kraków. Generator power lasting 96 hours on a full tank, city water supplemented by 15,000 L emergency tanks, HVAC heating, and a food store with 5-day reserves. Sanitary facilities for 450 people. Security personnel present at all times during alerts. Lifts available for disabled access."
        },
        new()
        {
            Name = "Schron Skotniki – Osiedle Podmiejskie",
            Address = "ul. Skotnicka 15, Kraków",
            Latitude = 50.0050, Longitude = 19.8900,
            Capacity = 55,
            Description = "Mały schron podmiejski. Woda ze studni. Brak prądu i ogrzewania. Nadaje się wyłącznie do krótkotrwałego schronienia."
        },
        new()
        {
            Name = "Schron Tonie – Remiza",
            Address = "ul. Toniecka 5, Tonie, Kraków",
            Latitude = 50.1100, Longitude = 19.9800,
            Capacity = 65,
            Description = "Village fire station shelter serving the Tonie area on the northern outskirts of Kraków. Generator power and mains water. No dedicated heating — portable units available on request from station staff. Capacity limited; residents from further areas should use larger shelters in the city centre."
        },
        new()
        {
            Name = "Schron Wróblowice – Szkoła",
            Address = "ul. Wróblowicka 40, Kraków",
            Latitude = 49.9980, Longitude = 19.9050,
            Capacity = 100,
            Description = "Szkolna piwnica na południu miasta. Elektryczność, woda i ogrzewanie elektryczne dostępne. Brak zapasów żywności."
        },
        new()
        {
            Name = "Schron Podchruście – Budynek Komunalny",
            Address = "ul. Podchruście 3, Kraków",
            Latitude = 50.0010, Longitude = 19.9700,
            Capacity = 85,
            Description = "Municipal building basement in southern Kraków. Only electricity from a small generator. No water piping in the basement — bottled water would need to be carried in. Basic shelter protection, not suitable for long-term stay."
        },
        new()
        {
            Name = "Schron Bronowice Wielkie – Dom Kultury",
            Address = "ul. Stachowicza 32, Kraków",
            Latitude = 50.0830, Longitude = 19.8800,
            Capacity = 160,
            Description = "Schronisko w Domu Kultury Bronowice. Prąd z sieci miejskiej i agregatu, woda, centralne ogrzewanie, zapasy żywności na 3 doby. Personel przeszkolony do roli wolontariuszy kryzysowych. Wyposażenie: koce, apteczki, latarki, radio awaryjne. Obiekt regularnie ćwiczony podczas lokalnych symulacji ewakuacji."
        },
        new()
        {
            Name = "Schron Zwierzyniec – Klasztor Kamedułów",
            Address = "ul. Kamedułów 1, Bielany, Kraków",
            Latitude = 50.0890, Longitude = 19.8700,
            Capacity = 40,
            Description = "Small monastic shelter at the Camaldolese monastery in Bielany. Water from the monastery well. Food from monastery stores. No electricity. Very limited capacity; priority for nearby residents. Access requires prior coordination with the monks."
        },
        new()
        {
            Name = "Schron Łęg – Nad Wisłą",
            Address = "ul. Łęgska 2, Kraków",
            Latitude = 50.0350, Longitude = 20.0400,
            Capacity = 120,
            Description = "Riverside shelter built into the Vistula levee infrastructure. Electricity and running water available. Note: during flood alerts this shelter may be inaccessible — verify the alert type before heading here. No heating installed."
        },
        new()
        {
            Name = "Schron Nowa Huta – Blok B31",
            Address = "os. Centrum B 31, Nowa Huta, Kraków",
            Latitude = 50.0740, Longitude = 20.0420,
            Capacity = 190,
            Description = "Typowy schron w bloku z wielkiej płyty na Nowej Hucie. Prąd, woda, ogrzewanie elektryczne. Brak żywności. Pojemność 190 osób."
        },
        new()
        {
            Name = "Schron Stare Podgórze – Kościół",
            Address = "ul. Zamenhoffa 5, Kraków",
            Latitude = 50.0460, Longitude = 19.9480,
            Capacity = 210,
            Description = "Church basement shelter in old Podgórze, maintained since the communist era. Adequate heating and lighting. Water from city mains. Food cache tended by parish volunteers — pasta, canned food, biscuits, and UHT milk for 72 hours. A small medical station is staffed by a retired nurse parishioner. Welcoming atmosphere; everyone is accepted."
        },
        new()
        {
            Name = "Schron Płaszów – Teren Przemysłowy",
            Address = "ul. Kamieńskiego 10, Kraków",
            Latitude = 50.0330, Longitude = 19.9780,
            Capacity = 300,
            Description = "Industrial zone bunker converted from a Cold War-era factory shelter. Large space with reinforced walls and electricity from an industrial generator. No water plumbing and no heating. Not recommended for families with small children — best suited for able-bodied adults during short-term emergencies."
        },
        new()
        {
            Name = "Schron Kobierzyn – Szpital Psychiatryczny",
            Address = "ul. Babińskiego 29, Kraków",
            Latitude = 50.0100, Longitude = 19.9130,
            Capacity = 250,
            Description = "Szpitalny schron z pełnym wyposażeniem. Elektryczność, woda, ogrzewanie i żywność z zasobów szpitalnych. Dostęp ogólny poza strefą pacjentów psychiatrycznych."
        },
        new()
        {
            Name = "Schron Kliny – Biblioteka Osiedlowa",
            Address = "ul. Klimeckiego 24, Kraków",
            Latitude = 50.0200, Longitude = 19.9320,
            Capacity = 75,
            Description = "Branch library basement converted to shelter use in 2020. Power from a small UPS and generator. Running water. No heating — the basement stays around 10°C. Some emergency blankets stored inside. Quiet and suitable for elderly people. No food on-site."
        },
        new()
        {
            Name = "Schron Wielicka – Centrum",
            Address = "ul. Wielicka 30, Kraków",
            Latitude = 50.0300, Longitude = 19.9700,
            Capacity = 155,
            Description = "Shelter on Wielicka street in central Podgórze. Full power from city grid and backup generator. Water available. Heated via electric radiators. Well-maintained with proper signage and an accessible entrance ramp. Medical kit on-site. No food stocks; a nearby community pantry operates as a complementary food point during extended alerts."
        },
        new()
        {
            Name = "Schron Niegoszowice",
            Address = "ul. Niegoszowska 7, Kraków",
            Latitude = 50.0020, Longitude = 19.8700,
            Capacity = 50,
            Description = "Stary schron podmiejski. Brak jakichkolwiek mediów. Wyłącznie murowana konstrukcja z solidnymi ścianami."
        },
        new()
        {
            Name = "Schron Cracovia – Stadion",
            Address = "ul. Józefa Kałuży 1, Kraków",
            Latitude = 50.0545, Longitude = 19.9105,
            Capacity = 500,
            Description = "Emergency shelter beneath the Cracovia football stadium, designated in the city's civil defense plan. Extensive space for up to 500 people. Power from stadium backup generators. Water from stadium supply. No dedicated heating — the concrete structure maintains adequate temperatures. Stadium staff are trained in evacuation procedures."
        },
        new()
        {
            Name = "Schron Wisła – Bulwar Czerwieński",
            Address = "Bulwar Czerwieński 1, Kraków",
            Latitude = 50.0590, Longitude = 19.9270,
            Capacity = 100,
            Description = "Schron przy bulwarze wiślanym. Elektryczność i woda dostępne. Brak ogrzewania. 100 miejsc."
        },
        new()
        {
            Name = "Schron Skałka – Kościół Paulinów",
            Address = "ul. Skałeczna 15, Kraków",
            Latitude = 50.0493, Longitude = 19.9417,
            Capacity = 180,
            Description = "The Pauline Fathers' monastery on Skałka maintains a substantial shelter in its historic vaulted cellars. Food stores from the monastery pantry cover 5 days, water comes from the garden well and city mains, electricity from the parish generator, and radiator heating keeps the space warm. Medical volunteer available. Open to all during alerts."
        },
        new()
        {
            Name = "Schron Rakowicka – Cmentarz Rakowicki",
            Address = "ul. Rakowicka 26, Kraków",
            Latitude = 50.0700, Longitude = 19.9600,
            Capacity = 130,
            Description = "Shelter in the administrative building basement of Rakowice Cemetery. Unusual location but structurally solid. Electricity available. No water in the basement — the water point is 50 m away at the caretaker's building. No heating. Access via the main cemetery office entrance."
        },
        new()
        {
            Name = "Schron Prądnik Biały – Szpital im. Żeromskiego",
            Address = "os. Na Skarpie 66, Kraków",
            Latitude = 50.0910, Longitude = 19.9500,
            Capacity = 200,
            Description = "Szpitalny schron z pełnym wyposażeniem: prąd, woda, ogrzewanie i żywność z zasobów szpitalnych. Pierwszeństwo dla pacjentów i personelu medycznego."
        },
        new()
        {
            Name = "Schron Krowodrza – Blok os. Krowodrza Górka",
            Address = "os. Krowodrza Górka 7, Kraków",
            Latitude = 50.0780, Longitude = 19.9110,
            Capacity = 140,
            Description = "Residential estate shelter upgraded in 2022 by the housing cooperative. New LED emergency lighting, HEPA-filtered ventilation, electric heating, and a reinforced blast door. Water from building supply. No food stored but residents can register dietary needs via the district emergency app. Maximum comfort capacity is closer to 100."
        },
        new()
        {
            Name = "Schron Olsza – Blok Komunalny",
            Address = "ul. Lublańska 22, Kraków",
            Latitude = 50.0800, Longitude = 19.9700,
            Capacity = 170,
            Description = "Schron blokowy. Prąd i woda. Brak ogrzewania. Pojemność 170 osób."
        },
        new()
        {
            Name = "Schron Salwator – Willa Historyczna",
            Address = "ul. Podchorążych 2, Kraków",
            Latitude = 50.0620, Longitude = 19.9150,
            Capacity = 55,
            Description = "Small shelter in the basement of a historic villa near Salwator, opened to the public under emergency decree. Nicely maintained, warm, with running water and electricity. Limited to 55 people — the villa's 12 residents take priority, remaining spots open to the public. No food on site."
        },
        new()
        {
            Name = "Schron Rybitwy – Osiedle Podtatrzańskie",
            Address = "ul. Rybitwy 20, Kraków",
            Latitude = 50.0200, Longitude = 20.0100,
            Capacity = 110,
            Description = "Schron osiedlowy z pełnym wyposażeniem: prąd, woda, ogrzewanie i zapasy żywności. Zarządzany przez spółdzielnię mieszkaniową."
        },
        new()
        {
            Name = "Schron Zesławice – Wieś",
            Address = "ul. Zesławicka 10, Kraków",
            Latitude = 50.1130, Longitude = 20.0200,
            Capacity = 45,
            Description = "Rural shelter on the northern edge of Kraków. Old but sound construction. Hand-pump well water is the only utility. No electricity or heating. Suitable for brief shelter from air threats."
        },
        new()
        {
            Name = "Schron Mistrzejowice – Kościół Wniebowzięcia",
            Address = "os. Tysiąclecia 86, Kraków",
            Latitude = 50.0960, Longitude = 19.9990,
            Capacity = 200,
            Description = "Church shelter managed by parish volunteers who keep it fully stocked. Power from parish generator, city water, gas heating. Food: soups, bread, and canned goods sufficient for 48 hours for 200 people. First aid kit and defibrillator on-site. Multilingual volunteers speak Polish, Ukrainian, and English. Pets allowed in the vestibule area."
        },
        new()
        {
            Name = "Schron Czyżyny – Akademik",
            Address = "ul. Lublańska 34, Kraków",
            Latitude = 50.0760, Longitude = 20.0090,
            Capacity = 230,
            Description = "Piwnica akademika studenckiego. Elektryczność, woda i ogrzewanie dostępne. Brak zapasów żywności."
        },
        new()
        {
            Name = "Schron Nowa Huta – Kombinat",
            Address = "ul. Ujastek 1, Kraków",
            Latitude = 50.0680, Longitude = 20.0660,
            Capacity = 800,
            Description = "The largest shelter in Kraków, built to Soviet industrial civil defense standards in the former ArcelorMittal steelworks. Massive blast doors and 5 m concrete walls. Capacity 800. Industrial generators with 7-day fuel supply, an on-site water purification plant, district heating, and a 7-day food store. Hazmat decontamination chamber. Medical station with full first aid and oxygen supplies. The primary shelter for eastern Kraków."
        },
        new()
        {
            Name = "Schron Krowodrza – Park Jordana",
            Address = "al. 3 Maja 12, Kraków",
            Latitude = 50.0623, Longitude = 19.9200,
            Capacity = 90,
            Description = "Schron pod parkiem miejskim. Dostępna tylko elektryczność z agregatu. Brak wody i ogrzewania. 90 miejsc."
        },
        new()
        {
            Name = "Schron Solvay – Dawna Fabryka",
            Address = "ul. Zakopiańska 62, Kraków",
            Latitude = 50.0170, Longitude = 19.9000,
            Capacity = 160,
            Description = "Industrial heritage shelter at the former Solvay soda ash factory — the wartime workplace of Karol Wojtyła. Electricity from an old but functional generator, and a dedicated water tank. No heating; the stable brick construction keeps a consistent temperature. Historical information boards give context to this significant location."
        },
        new()
        {
            Name = "Schron Wola Justowska – Willa",
            Address = "ul. Królowej Jadwigi 200, Kraków",
            Latitude = 50.0800, Longitude = 19.8850,
            Capacity = 70,
            Description = "Shelter in an upscale residential villa area. Electricity, running water, and heating all available. Capacity 70. Access via the garden gate during alerts. No food stored on-site."
        },
        new()
        {
            Name = "Schron Prądnik Biały – Szkoła nr 8",
            Address = "ul. Ułanów 17, Kraków",
            Latitude = 50.0950, Longitude = 19.9430,
            Capacity = 145,
            Description = "Szkolna piwnica służąca jako schron dla dzielnicy. Prąd z agregatu, woda miejska, ogrzewanie elektryczne. Koce i apteczki dostępne na miejscu. Brak zapasów żywności."
        },
        new()
        {
            Name = "Schron Olsza – Kościół Miłosierdzia",
            Address = "ul. Olszańska 5, Kraków",
            Latitude = 50.0775, Longitude = 19.9770,
            Capacity = 130,
            Description = "Parafialne schronisko z zapasami żywności na 48 godzin. Zarządzane przez wolontariuszy parafii. Prąd, woda i ogrzewanie dostępne."
        },
        new()
        {
            Name = "Schron Podgórze – Centrum Biznesowe",
            Address = "ul. Zabłocie 25, Kraków",
            Latitude = 50.0400, Longitude = 19.9720,
            Capacity = 320,
            Description = "Modern office basement shelter in the Zabłocie business district, maintained during a 2015 redevelopment. Earthquake-proofed concrete shell. Electricity from rooftop solar + UPS + diesel backup. Water from building supply. Heating via HVAC. No food stocks — the ground-floor café has an agreement to provide emergency catering during prolonged alerts."
        },
        new()
        {
            Name = "Schron Grębałów – Blok",
            Address = "os. Grębałów 14, Kraków",
            Latitude = 50.0900, Longitude = 20.0500,
            Capacity = 100,
            Description = "Schron blokowy na wschodzie miasta. Dostępna tylko elektryczność z agregatu. Brak wody i ogrzewania."
        },
        new()
        {
            Name = "Schron Pychowice – Instytut Botaniczny",
            Address = "ul. Lubostroń 46, Kraków",
            Latitude = 50.0230, Longitude = 19.8960,
            Capacity = 85,
            Description = "Shelter beneath the Botanical Institute on the southern edge of the city. Power from institution backup generator. Water from the laboratory supply (potable). No heating. Quiet, well-organized space; staff familiar with emergency protocols."
        },
        new()
        {
            Name = "Schron Dąbie – Plaża Nad Wisłą",
            Address = "ul. Bulwarowa 10, Kraków",
            Latitude = 50.0680, Longitude = 20.0310,
            Capacity = 75,
            Description = "Shelter in a reinforced basement near the Dąbie beach area. Power and running water available. No heating. Flood risk in certain scenarios — verify alert type before use. Capacity 75."
        },
        new()
        {
            Name = "Schron Nowa Huta – Szkoła Policealna",
            Address = "os. Willowe 30, Kraków",
            Latitude = 50.0630, Longitude = 20.0590,
            Capacity = 165,
            Description = "Schron szkolny na terenie Nowej Huty. Elektryczność, woda i ogrzewanie zapewnione. Brak własnych zapasów żywności."
        },
        new()
        {
            Name = "Schron Dębniki – Kościół Redemptorystów",
            Address = "ul. Zamoyskiego 56, Kraków",
            Latitude = 50.0400, Longitude = 19.9160,
            Capacity = 175,
            Description = "One of the best-maintained shelters in the Dębniki area. Monthly checks by parish community. Electricity from church generator, city water, gas heating. Food stockpile — soups, crackers, and baby food — covers 3 days for 175 people. Multilingual welcome (Polish, English, Ukrainian). Quiet zone for nursing mothers and infants. Monthly practice drill."
        },
        new()
        {
            Name = "Schron Swoszowice – Basen",
            Address = "ul. Reymana 20, Kraków",
            Latitude = 49.9970, Longitude = 19.9310,
            Capacity = 280,
            Description = "Schron w podziemiach kompleksu sportowego z basenem. Duża pojemność, prąd, woda i ogrzewanie z systemu basenowego. Brak dedykowanych zapasów żywności."
        },
        new()
        {
            Name = "Schron Klucze – Teren Podmiejski",
            Address = "ul. Klucze 5, Kraków",
            Latitude = 50.1020, Longitude = 19.9300,
            Capacity = 60,
            Description = "Suburban shelter on the northern edge. Water from a gravity-fed tank. No electricity or heating. Suitable for short-term use only."
        },
        new()
        {
            Name = "Schron Azory – Centrum",
            Address = "ul. Józefa Conrada 18, Kraków",
            Latitude = 50.0850, Longitude = 19.9060,
            Capacity = 190,
            Description = "Osiedlowe schronisko Azory zarządzane przez miasto. Prąd z agregatu, woda miejska, centralne ogrzewanie, zapasy żywności na 3 doby. Strefa dla zwierząt domowych. Personel dyżurny, apteczka i defibrylator AED na miejscu."
        },
        new()
        {
            Name = "Schron Nowa Huta – Medyk",
            Address = "os. Złotej Jesieni 1, Kraków",
            Latitude = 50.0650, Longitude = 20.0800,
            Capacity = 120,
            Description = "Medical school basement shelter. Power and water available. No heating. Used as a triage overflow facility during mass casualty events."
        },
        new()
        {
            Name = "Schron Krowodrza – Centrum Administracyjne",
            Address = "ul. Przy Rondzie 6, Kraków",
            Latitude = 50.0690, Longitude = 19.9170,
            Capacity = 210,
            Description = "City-funded district administrative shelter with dual power supply (city grid + generator), municipal water, and central heating. Emergency communication terminal on-site managed by civil defense staff. No permanent food stores but distribution logistics are pre-planned with nearby suppliers."
        },
        new()
        {
            Name = "Schron Podgórze – Tramwajowa",
            Address = "ul. Kalwaryjska 9, Kraków",
            Latitude = 50.0440, Longitude = 19.9390,
            Capacity = 135,
            Description = "Schron przy zajezdni tramwajowej. Elektryczność i woda dostępne. Brak ogrzewania. Pojemność 135 osób."
        },
        new()
        {
            Name = "Schron Olszanica – Góra",
            Address = "ul. Olszanicka 20, Kraków",
            Latitude = 50.0150, Longitude = 19.9100,
            Capacity = 70,
            Description = "Small hillside shelter, historically used as a storage bunker. Water from a spring-fed pipe. No electricity or heating. Sound masonry construction."
        },
        new()
        {
            Name = "Schron Bestwinka – Willa",
            Address = "ul. Bestwińska 4, Kraków",
            Latitude = 50.0070, Longitude = 19.9780,
            Capacity = 40,
            Description = "Małe prywatne schronisko z dostępem do prądu z agregatu. Brak wody i ogrzewania. Bardzo ograniczona pojemność."
        },
        new()
        {
            Name = "Schron Wielka Wola – Blok",
            Address = "ul. Wielka Wola 15, Kraków",
            Latitude = 50.0940, Longitude = 19.9150,
            Capacity = 105,
            Description = "Block shelter in northwest Kraków. Electricity and water available. No heating — insulation is good and the space stays reasonably warm. Adequate for medium-length stays."
        },
        new()
        {
            Name = "Schron Bibice – Wieś",
            Address = "ul. Bibicka 8, Bibice, Kraków",
            Latitude = 50.1200, Longitude = 19.9500,
            Capacity = 50,
            Description = "Wiejski bunkier. Brak mediów. Wyłącznie schronienie przed odłamkami i falą uderzeniową."
        },
        new()
        {
            Name = "Schron Bronowice – Akademik Studentów",
            Address = "ul. Armii Krajowej 5, Kraków",
            Latitude = 50.0790, Longitude = 19.8960,
            Capacity = 240,
            Description = "University dormitory complex basement. Electricity from campus microgrid, city water, and heating from the campus boiler house. Basic amenities: toilets, some cots, emergency lighting. The dormitory cafeteria provides emergency meals during extended alerts. Wi-Fi available on campus emergency power."
        },
        new()
        {
            Name = "Schron Borek Fałęcki – Osiedle",
            Address = "ul. Borowska 13, Kraków",
            Latitude = 50.0060, Longitude = 19.9230,
            Capacity = 95,
            Description = "Schron spółdzielczy z elektrycznością, wodą, ogrzewaniem i zapasami żywności na 2 doby."
        },
        new()
        {
            Name = "Schron Nowa Huta – Os. Słoneczne",
            Address = "os. Słoneczne 15, Kraków",
            Latitude = 50.0810, Longitude = 20.0550,
            Capacity = 155,
            Description = "Standard Nowa Huta estate shelter, well maintained by the housing co-op. Power, water, and heating all in working order. No food reserves. Capacity 155."
        },
        new()
        {
            Name = "Schron Wola Duchacka – Szpital Dziecięcy",
            Address = "ul. Wielicka 265, Kraków",
            Latitude = 50.0130, Longitude = 19.9640,
            Capacity = 180,
            Description = "Szpital dziecięcy — schron z personelem medycznym. Elektryczność, woda, ogrzewanie i żywność szpitalna. Priorytet dla pacjentów pediatrycznych; ogólnodostępny, jeśli pojemność pozwala."
        },
        new()
        {
            Name = "Schron Prądnik – Dom Opieki",
            Address = "ul. Prądnicka 80, Kraków",
            Latitude = 50.0880, Longitude = 19.9370,
            Capacity = 100,
            Description = "Schron przy domu opieki. Pełne wyposażenie: prąd, woda, ogrzewanie, żywność. Priorytet dla seniorów i osób z niepełnosprawnościami."
        },
        new()
        {
            Name = "Schron Nowe Bieżanowo",
            Address = "os. Nowe Bieżanowo 5, Kraków",
            Latitude = 50.0100, Longitude = 20.0310,
            Capacity = 130,
            Description = "Estate shelter in Nowe Bieżanowo. Power from building grid and small generator. Water from municipal supply. No dedicated heating — the underground location stays relatively warm. Last formally inspected in 2023."
        },
        new()
        {
            Name = "Schron Prokocim – Blok E",
            Address = "os. Złotej Jesieni 45, Kraków",
            Latitude = 50.0210, Longitude = 20.0450,
            Capacity = 120,
            Description = "Schron blokowy w Prokocimiu. Dostępna tylko elektryczność z agregatu. Brak wody i ogrzewania."
        },
        new()
        {
            Name = "Schron Zwierzyniec – Szkoła im. Słowackiego",
            Address = "ul. Słowackiego 30, Kraków",
            Latitude = 50.0668, Longitude = 19.9180,
            Capacity = 140,
            Description = "School basement shelter, well-suited for families. Electricity, water, and heating all available. Space for approximately 20 cots alongside standard seating. No food stocked."
        },
        new()
        {
            Name = "Schron Bieżanów – Nowy Bieżanów Blok A",
            Address = "ul. Bieżanowska 110, Kraków",
            Latitude = 50.0165, Longitude = 20.0100,
            Capacity = 90,
            Description = "Mały schron osiedlowy. Prąd, woda i ogrzewanie elektryczne dostępne. Brak zapasów żywności."
        },
        new()
        {
            Name = "Schron Łęg – Ogródki Działkowe",
            Address = "ul. Saska 10, Kraków",
            Latitude = 50.0310, Longitude = 20.0540,
            Capacity = 60,
            Description = "Schron przy ogrodach działkowych. Woda z ujęcia zbiorowego. Brak prądu i ogrzewania."
        },
        new()
        {
            Name = "Schron Nowa Huta – Szkoła nr 115",
            Address = "os. Handlowe 7, Kraków",
            Latitude = 50.0720, Longitude = 20.0360,
            Capacity = 200,
            Description = "Schronisko szkolne z pełnym wyposażeniem: elektryczność, woda, ogrzewanie i żywność na 2 doby. W czasie alertu dyżuruje psycholog kryzysowy."
        },
        new()
        {
            Name = "Schron Bonarka – Centrum Handlowe",
            Address = "ul. Kamieńskiego 11, Kraków",
            Latitude = 50.0257, Longitude = 19.9493,
            Capacity = 550,
            Description = "Major shopping mall basement in southern Kraków. One of the most spacious shelters in the area. Dual generator rated for 120 hours, city water plus 20,000 L tanks, HVAC heating and cooling, and mall food and drink stores available under emergency protocol. Toilets, baby changing facilities, full disabled access. Civil defense liaison on site. Parking also accessible."
        },
        new()
        {
            Name = "Schron Grzybowice – Podmiejskie",
            Address = "ul. Grzybowice 3, Kraków",
            Latitude = 50.1080, Longitude = 19.9680,
            Capacity = 55,
            Description = "Wiejski schron na obrzeżach miasta. Woda ze studni. Brak prądu i ogrzewania."
        },
        new()
        {
            Name = "Schron Podgórze – Muzeum Fotografii",
            Address = "ul. Rakowicka 22a, Kraków",
            Latitude = 50.0620, Longitude = 19.9520,
            Capacity = 85,
            Description = "Museum basement shelter with well-constructed brick vaulting. Power from museum backup. Water from city supply. No heating. Capacity limited to 85. The shelter area is kept clear of artifacts and obstructions."
        },
        new()
        {
            Name = "Schron Krowodrza – Centrum Sportu",
            Address = "ul. Reymonta 22, Kraków",
            Latitude = 50.0710, Longitude = 19.9070,
            Capacity = 260,
            Description = "Sports centre basement shelter with electricity, running water, and heating. Large capacity with wide corridors and an accessible entrance. No food stores."
        },
        new()
        {
            Name = "Schron Wadów – Blok",
            Address = "ul. Wadowicka 8a, Kraków",
            Latitude = 50.0280, Longitude = 19.9230,
            Capacity = 115,
            Description = "Schron blokowy na południu miasta. Prąd z sieci miejskiej, woda z wodociągów. Brak ogrzewania — wymagany własny koc lub śpiwór zimowy."
        },
        new()
        {
            Name = "Schron Dębniki – Rynek Dębniki",
            Address = "ul. Rynek Dębnicki 1, Kraków",
            Latitude = 50.0480, Longitude = 19.9230,
            Capacity = 155,
            Description = "Community shelter under the Dębniki main square, managed jointly by the local parish and district office. Dedicated shelter generator, water from well and city mains, gas heating. Food: pasta, canned soup, rice, and water sachets for 72 hours for 155 people. Entry from the north side of the square."
        },
        new()
        {
            Name = "Schron Stary Kleparz – Hala Targowa",
            Address = "ul. Basztowa 3, Kraków",
            Latitude = 50.0672, Longitude = 19.9432,
            Capacity = 170,
            Description = "Basement of the old market hall. Electricity and water available. No heating — stable temperature from the stone construction. Permanent food stocks maintained due to proximity to the fresh produce market; traders have an informal agreement to supply fresh produce during extended emergencies."
        },
        new()
        {
            Name = "Schron Przylasek Rusiecki – Osiedle",
            Address = "ul. Rusiecka 15, Kraków",
            Latitude = 50.0550, Longitude = 20.0900,
            Capacity = 80,
            Description = "Schron na wschodnich obrzeżach gminy. Elektryczność z agregatu. Brak wody i ogrzewania."
        },
        new()
        {
            Name = "Schron Krowodrza – Os. Prądnik Biały Wschód",
            Address = "ul. ks. Meiera 14, Kraków",
            Latitude = 50.0870, Longitude = 19.9560,
            Capacity = 145,
            Description = "Estate shelter managed by the housing association. Electricity, water, and heating available. No food stores. Capacity 145."
        },
        new()
        {
            Name = "Schron Mistrzejowice – Centrum Kultury",
            Address = "os. Mistrzejowice 1, Kraków",
            Latitude = 50.0980, Longitude = 20.0100,
            Capacity = 220,
            Description = "Schronisko w Centrum Kultury Mistrzejowice — centrum koordynacji wolontariatu dla dzielnicy. Prąd, woda, ogrzewanie i zapasy żywności na 3 doby. Psycholog i pielęgniarka dyżurna. Strefa dla dzieci z kocami i zabawkami."
        },
        new()
        {
            Name = "Schron Nowa Huta – Os. Przy Arce",
            Address = "os. Przy Arce 8, Kraków",
            Latitude = 50.0770, Longitude = 20.0450,
            Capacity = 135,
            Description = "Standardowy schron blokowy. Prąd i woda. Brak ogrzewania."
        },
        new()
        {
            Name = "Schron Białe Morza – Hałda",
            Address = "ul. Igołomska 25, Kraków",
            Latitude = 50.0500, Longitude = 20.1050,
            Capacity = 70,
            Description = "Industrial area shelter near the former soda plant. Electricity from a generator. No water or heating. Air quality monitoring is recommended due to industrial legacy in the area. Suitable for short stays only."
        },
        new()
        {
            Name = "Schron Piaski Wielkie – Osiedle",
            Address = "ul. Piaski Wielkie 40, Kraków",
            Latitude = 50.0050, Longitude = 20.0000,
            Capacity = 110,
            Description = "Schron osiedlowy z elektrycznością, wodą i ogrzewaniem. Brak zapasów żywności."
        },
        new()
        {
            Name = "Schron Wola Duchacka Wschód",
            Address = "ul. Tarnowska 45, Kraków",
            Latitude = 50.0070, Longitude = 19.9900,
            Capacity = 90,
            Description = "Małe schronisko podmiejskie. Wyłącznie prąd z agregatu. Brak innych mediów."
        },
        new()
        {
            Name = "Schron Górka Narodowa – Blok",
            Address = "ul. Górka Narodowa 75, Kraków",
            Latitude = 50.1020, Longitude = 19.9900,
            Capacity = 120,
            Description = "Residential block shelter on Górka Narodowa estate. Electricity, water, and heating all functioning. No food stored. Access via building basement."
        },
        new()
        {
            Name = "Schron Wróblowice – Kościół",
            Address = "ul. Wróblowicka 60, Kraków",
            Latitude = 49.9950, Longitude = 19.9060,
            Capacity = 75,
            Description = "Parafialne schronisko na południu gminy. Woda z sieci miejskiej i zapasy żywności prowadzone przez parafian. Brak prądu i ogrzewania."
        },
        new()
        {
            Name = "Schron Czyżyny – Hala Widowiskowa",
            Address = "ul. Lublańska 65, Kraków",
            Latitude = 50.0720, Longitude = 20.0230,
            Capacity = 300,
            Description = "Event hall underground level repurposed as shelter. Electricity, water, and heating available. Large open space with folding partitions for privacy sections. Capacity 300. No food."
        },
        new()
        {
            Name = "Schron Bronowice – Osiedle Krowoderskie",
            Address = "os. Krowoderskie 22, Kraków",
            Latitude = 50.0760, Longitude = 19.9010,
            Capacity = 140,
            Description = "Schron na osiedlu Krowoderskie. Prąd i woda dostępne. Brak ogrzewania."
        },
        new()
        {
            Name = "Schron Łężce – Punkt Medyczny",
            Address = "ul. Łężce 3, Kraków",
            Latitude = 50.0400, Longitude = 20.0750,
            Capacity = 55,
            Description = "Mały punkt medyczno-schronowy. Pełne wyposażenie: prąd, woda, ogrzewanie i zapasy żywności. Priorytet dla rannych i osób starszych."
        },
        new()
        {
            Name = "Schron Brzeska – Remiza Straży",
            Address = "ul. Brzeska 20, Kraków",
            Latitude = 50.0560, Longitude = 20.0800,
            Capacity = 80,
            Description = "Fire station shelter on the eastern edge of Kraków municipality. Power and water available. No permanent heating. Fire crew present 24/7 and will assist shelter users."
        },
        new()
        {
            Name = "Schron Centrum – Dworzec Główny",
            Address = "pl. Kolejowy 1, Kraków",
            Latitude = 50.0679, Longitude = 19.9478,
            Capacity = 700,
            Description = "The main railway station underground level serves as one of the city's primary civilian shelters, located beneath the Galeria Krakowska shopping complex. Capacity up to 700. Triple-redundancy power supply, running water, HVAC heating and ventilation, and access to mall food supplies. Station staff trained in emergency procedures and continuously on duty. Multiple entrances and exits; fully accessible for all mobility levels. Primary coordination point for central Kraków evacuations."
        },
        new()
        {
            Name = "Schron Wiosenna – Blok Nowy",
            Address = "ul. Wiosenna 12, Kraków",
            Latitude = 50.0610, Longitude = 20.0200,
            Capacity = 95,
            Description = "Newly built residential block basement. Electricity and water available. Heating has not been installed yet. Suitable for short-term use."
        },
        new()
        {
            Name = "Schron Tyniec – Opactwo",
            Address = "ul. Benedyktyńska 37, Tyniec, Kraków",
            Latitude = 50.0155, Longitude = 19.8340,
            Capacity = 100,
            Description = "Benedictine Abbey in Tyniec maintains a shelter in its historic cellars for the surrounding community. Electricity from the abbey generator, spring water, heating, and monastic food stores for 100 people for 5 days. Medical first aid available. The abbey bell will serve as an air raid signal if electronic sirens fail. Exceptional structural integrity — 10th-century stone construction."
        },
        new()
        {
            Name = "Schron Sidzina – Wieś Podmiejska",
            Address = "ul. Sidzińska 4, Sidzina, Kraków",
            Latitude = 49.9800, Longitude = 19.9680,
            Capacity = 40,
            Description = "Wiejski schron na południu gminy. Brak mediów. Wyłącznie betonowa konstrukcja ochronna."
        },
        new()
        {
            Name = "Schron Libertów – Podmiejski",
            Address = "ul. Libertowska 8, Libertów, Kraków",
            Latitude = 49.9870, Longitude = 19.9200,
            Capacity = 60,
            Description = "Małe podmiejskie schronisko z elektrycznością z małego agregatu. Brak wody i ogrzewania."
        },
        new()
        {
            Name = "Schron Kokotów – Wieś",
            Address = "ul. Kokotowska 2, Kokotów",
            Latitude = 49.9950, Longitude = 20.0500,
            Capacity = 35,
            Description = "Rural shelter south-east of Kraków. Water from a communal tap is the only utility. Basic structural protection."
        },
        new()
        {
            Name = "Укриття №1 - вул. Чорновола",
            Address = "вул. Чорновола 31, Івано-Франківськ",
            Latitude = 48.9465, Longitude = 24.7223,
            Capacity = 100,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №2 - вул. Набережна",
            Address = "вул. Набережна 129, Івано-Франківськ",
            Latitude = 48.9128, Longitude = 24.7363,
            Capacity = 300,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №3 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 62, Івано-Франківськ",
            Latitude = 48.9015, Longitude = 24.7241,
            Capacity = 500,
            Description = "Підвальне приміщення бібліотеки. Є книги та настільні ігри. Тихо та спокійно."
        },
        new()
        {
            Name = "Укриття №4 - вул. Галицька",
            Address = "вул. Галицька 95, Івано-Франківськ",
            Latitude = 48.8981, Longitude = 24.6931,
            Capacity = 150,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №5 - вул. Бандери",
            Address = "вул. Бандери 124, Івано-Франківськ",
            Latitude = 48.9460, Longitude = 24.6951,
            Capacity = 300,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №6 - вул. Миру",
            Address = "вул. Миру 106, Івано-Франківськ",
            Latitude = 48.9498, Longitude = 24.7263,
            Capacity = 150,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №7 - вул. Хоткевича",
            Address = "вул. Хоткевича 149, Івано-Франківськ",
            Latitude = 48.9096, Longitude = 24.7164,
            Capacity = 50,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №8 - вул. Чорновола",
            Address = "вул. Чорновола 74, Івано-Франківськ",
            Latitude = 48.9144, Longitude = 24.7317,
            Capacity = 200,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №9 - вул. Мельника",
            Address = "вул. Мельника 103, Івано-Франківськ",
            Latitude = 48.9293, Longitude = 24.7194,
            Capacity = 200,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №10 - вул. Мельника",
            Address = "вул. Мельника 27, Івано-Франківськ",
            Latitude = 48.9284, Longitude = 24.7367,
            Capacity = 500,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №11 - вул. Коновальця",
            Address = "вул. Коновальця 146, Івано-Франківськ",
            Latitude = 48.9477, Longitude = 24.7276,
            Capacity = 150,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №12 - вул. Січових Стрільців",
            Address = "вул. Січових Стрільців 118, Івано-Франківськ",
            Latitude = 48.9477, Longitude = 24.6884,
            Capacity = 50,
            Description = "Підземний паркінг новобудови. Сухе та тепле приміщення. Можна з тваринами."
        },
        new()
        {
            Name = "Укриття №13 - вул. Коновальця",
            Address = "вул. Коновальця 111, Івано-Франківськ",
            Latitude = 48.9138, Longitude = 24.7317,
            Capacity = 150,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №14 - вул. Коновальця",
            Address = "вул. Коновальця 132, Івано-Франківськ",
            Latitude = 48.9414, Longitude = 24.6812,
            Capacity = 100,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №15 - вул. Миру",
            Address = "вул. Миру 109, Івано-Франківськ",
            Latitude = 48.9389, Longitude = 24.6820,
            Capacity = 300,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №16 - вул. Сахарова",
            Address = "вул. Сахарова 115, Івано-Франківськ",
            Latitude = 48.9318, Longitude = 24.6926,
            Capacity = 500,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №17 - вул. Тичини",
            Address = "вул. Тичини 27, Івано-Франківськ",
            Latitude = 48.9461, Longitude = 24.7089,
            Capacity = 50,
            Description = "Підвальне приміщення бібліотеки. Є книги та настільні ігри. Тихо та спокійно."
        },
        new()
        {
            Name = "Укриття №18 - вул. Бандери",
            Address = "вул. Бандери 122, Івано-Франківськ",
            Latitude = 48.9167, Longitude = 24.6917,
            Capacity = 300,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №19 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 69, Івано-Франківськ",
            Latitude = 48.9249, Longitude = 24.6877,
            Capacity = 150,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №20 - вул. Хоткевича",
            Address = "вул. Хоткевича 30, Івано-Франківськ",
            Latitude = 48.8998, Longitude = 24.7056,
            Capacity = 50,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №21 - вул. Чорновола",
            Address = "вул. Чорновола 20, Івано-Франківськ",
            Latitude = 48.9472, Longitude = 24.7176,
            Capacity = 300,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №22 - вул. Мельника",
            Address = "вул. Мельника 30, Івано-Франківськ",
            Latitude = 48.9184, Longitude = 24.6856,
            Capacity = 150,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №23 - вул. Мельника",
            Address = "вул. Мельника 15, Івано-Франківськ",
            Latitude = 48.9282, Longitude = 24.7211,
            Capacity = 200,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №24 - вул. Мазепи",
            Address = "вул. Мазепи 33, Івано-Франківськ",
            Latitude = 48.9076, Longitude = 24.7321,
            Capacity = 100,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №25 - вул. Чорновола",
            Address = "вул. Чорновола 33, Івано-Франківськ",
            Latitude = 48.9024, Longitude = 24.7099,
            Capacity = 100,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №26 - вул. Бандери",
            Address = "вул. Бандери 22, Івано-Франківськ",
            Latitude = 48.9366, Longitude = 24.7265,
            Capacity = 100,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №27 - вул. Довга",
            Address = "вул. Довга 42, Івано-Франківськ",
            Latitude = 48.9057, Longitude = 24.6890,
            Capacity = 200,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №28 - вул. Набережна",
            Address = "вул. Набережна 79, Івано-Франківськ",
            Latitude = 48.9419, Longitude = 24.7263,
            Capacity = 150,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №29 - вул. Бельведерська",
            Address = "вул. Бельведерська 91, Івано-Франківськ",
            Latitude = 48.9401, Longitude = 24.7162,
            Capacity = 500,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №30 - вул. Мазепи",
            Address = "вул. Мазепи 113, Івано-Франківськ",
            Latitude = 48.8957, Longitude = 24.7120,
            Capacity = 50,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №31 - вул. Набережна",
            Address = "вул. Набережна 36, Івано-Франківськ",
            Latitude = 48.9169, Longitude = 24.7249,
            Capacity = 50,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №32 - вул. Франка",
            Address = "вул. Франка 121, Івано-Франківськ",
            Latitude = 48.9466, Longitude = 24.7253,
            Capacity = 500,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №33 - вул. Шевченка",
            Address = "вул. Шевченка 121, Івано-Франківськ",
            Latitude = 48.9168, Longitude = 24.7230,
            Capacity = 300,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №34 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 95, Івано-Франківськ",
            Latitude = 48.9415, Longitude = 24.7142,
            Capacity = 150,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №35 - вул. Мельника",
            Address = "вул. Мельника 64, Івано-Франківськ",
            Latitude = 48.8938, Longitude = 24.6809,
            Capacity = 500,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №36 - вул. Франка",
            Address = "вул. Франка 12, Івано-Франківськ",
            Latitude = 48.9225, Longitude = 24.7296,
            Capacity = 200,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №37 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 41, Івано-Франківськ",
            Latitude = 48.9189, Longitude = 24.7080,
            Capacity = 500,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №38 - вул. Бельведерська",
            Address = "вул. Бельведерська 47, Івано-Франківськ",
            Latitude = 48.8949, Longitude = 24.7200,
            Capacity = 100,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №39 - вул. Незалежності",
            Address = "вул. Незалежності 132, Івано-Франківськ",
            Latitude = 48.9442, Longitude = 24.7130,
            Capacity = 300,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №40 - вул. Довга",
            Address = "вул. Довга 89, Івано-Франківськ",
            Latitude = 48.9368, Longitude = 24.7268,
            Capacity = 200,
            Description = "Підземний паркінг новобудови. Сухе та тепле приміщення. Можна з тваринами."
        },
        new()
        {
            Name = "Укриття №41 - вул. Франка",
            Address = "вул. Франка 109, Івано-Франківськ",
            Latitude = 48.9251, Longitude = 24.7229,
            Capacity = 300,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №42 - вул. Незалежності",
            Address = "вул. Незалежності 131, Івано-Франківськ",
            Latitude = 48.9067, Longitude = 24.6884,
            Capacity = 200,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №43 - вул. Тичини",
            Address = "вул. Тичини 149, Івано-Франківськ",
            Latitude = 48.9486, Longitude = 24.7173,
            Capacity = 200,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №44 - вул. Коновальця",
            Address = "вул. Коновальця 62, Івано-Франківськ",
            Latitude = 48.9127, Longitude = 24.6929,
            Capacity = 50,
            Description = "Підземний паркінг новобудови. Сухе та тепле приміщення. Можна з тваринами."
        },
        new()
        {
            Name = "Укриття №45 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 107, Івано-Франківськ",
            Latitude = 48.8955, Longitude = 24.7144,
            Capacity = 300,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №46 - вул. Мельника",
            Address = "вул. Мельника 37, Івано-Франківськ",
            Latitude = 48.9341, Longitude = 24.7312,
            Capacity = 200,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №47 - вул. Франка",
            Address = "вул. Франка 30, Івано-Франківськ",
            Latitude = 48.9477, Longitude = 24.7202,
            Capacity = 150,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №48 - вул. Незалежності",
            Address = "вул. Незалежності 20, Івано-Франківськ",
            Latitude = 48.9130, Longitude = 24.7021,
            Capacity = 300,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №49 - вул. Будівельників",
            Address = "вул. Будівельників 97, Івано-Франківськ",
            Latitude = 48.9364, Longitude = 24.6954,
            Capacity = 150,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №50 - вул. Хоткевича",
            Address = "вул. Хоткевича 138, Івано-Франківськ",
            Latitude = 48.9447, Longitude = 24.6962,
            Capacity = 300,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №51 - вул. Незалежності",
            Address = "вул. Незалежності 61, Івано-Франківськ",
            Latitude = 48.9154, Longitude = 24.6895,
            Capacity = 200,
            Description = "Підвальне приміщення бібліотеки. Є книги та настільні ігри. Тихо та спокійно."
        },
        new()
        {
            Name = "Укриття №52 - вул. Чорновола",
            Address = "вул. Чорновола 96, Івано-Франківськ",
            Latitude = 48.9452, Longitude = 24.7254,
            Capacity = 100,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №53 - вул. Хоткевича",
            Address = "вул. Хоткевича 42, Івано-Франківськ",
            Latitude = 48.9285, Longitude = 24.7270,
            Capacity = 50,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №54 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 81, Івано-Франківськ",
            Latitude = 48.9053, Longitude = 24.6970,
            Capacity = 150,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №55 - вул. Січових Стрільців",
            Address = "вул. Січових Стрільців 42, Івано-Франківськ",
            Latitude = 48.9255, Longitude = 24.7166,
            Capacity = 50,
            Description = "Підземний паркінг новобудови. Сухе та тепле приміщення. Можна з тваринами."
        },
        new()
        {
            Name = "Укриття №56 - вул. Галицька",
            Address = "вул. Галицька 46, Івано-Франківськ",
            Latitude = 48.9405, Longitude = 24.7040,
            Capacity = 300,
            Description = "Підвальне приміщення бібліотеки. Є книги та настільні ігри. Тихо та спокійно."
        },
        new()
        {
            Name = "Укриття №57 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 15, Івано-Франківськ",
            Latitude = 48.9429, Longitude = 24.7248,
            Capacity = 50,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №58 - вул. Бельведерська",
            Address = "вул. Бельведерська 58, Івано-Франківськ",
            Latitude = 48.9086, Longitude = 24.7310,
            Capacity = 100,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №59 - вул. Миру",
            Address = "вул. Миру 117, Івано-Франківськ",
            Latitude = 48.9500, Longitude = 24.7043,
            Capacity = 150,
            Description = "Підземний паркінг новобудови. Сухе та тепле приміщення. Можна з тваринами."
        },
        new()
        {
            Name = "Укриття №60 - вул. Галицька",
            Address = "вул. Галицька 109, Івано-Франківськ",
            Latitude = 48.9265, Longitude = 24.7318,
            Capacity = 500,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №61 - вул. Хоткевича",
            Address = "вул. Хоткевича 115, Івано-Франківськ",
            Latitude = 48.9448, Longitude = 24.7322,
            Capacity = 500,
            Description = "Підвальне приміщення бібліотеки. Є книги та настільні ігри. Тихо та спокійно."
        },
        new()
        {
            Name = "Укриття №62 - вул. Хоткевича",
            Address = "вул. Хоткевича 86, Івано-Франківськ",
            Latitude = 48.9201, Longitude = 24.6813,
            Capacity = 50,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №63 - вул. Бандери",
            Address = "вул. Бандери 91, Івано-Франківськ",
            Latitude = 48.8954, Longitude = 24.6863,
            Capacity = 50,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №64 - вул. Галицька",
            Address = "вул. Галицька 42, Івано-Франківськ",
            Latitude = 48.9135, Longitude = 24.7206,
            Capacity = 100,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №65 - вул. Коновальця",
            Address = "вул. Коновальця 50, Івано-Франківськ",
            Latitude = 48.9110, Longitude = 24.7011,
            Capacity = 300,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №66 - вул. Шевченка",
            Address = "вул. Шевченка 87, Івано-Франківськ",
            Latitude = 48.8973, Longitude = 24.7228,
            Capacity = 100,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №67 - вул. Коновальця",
            Address = "вул. Коновальця 111, Івано-Франківськ",
            Latitude = 48.9244, Longitude = 24.7281,
            Capacity = 50,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №68 - вул. Довга",
            Address = "вул. Довга 87, Івано-Франківськ",
            Latitude = 48.9456, Longitude = 24.7154,
            Capacity = 50,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №69 - вул. Тичини",
            Address = "вул. Тичини 48, Івано-Франківськ",
            Latitude = 48.9444, Longitude = 24.7326,
            Capacity = 50,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №70 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 73, Івано-Франківськ",
            Latitude = 48.9087, Longitude = 24.7251,
            Capacity = 150,
            Description = "Сховище у школі. Обладнане вентиляцією та санвузлом. Є медичний куточок."
        },
        new()
        {
            Name = "Укриття №71 - вул. Мазепи",
            Address = "вул. Мазепи 36, Івано-Франківськ",
            Latitude = 48.8957, Longitude = 24.7304,
            Capacity = 200,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №72 - вул. Бандери",
            Address = "вул. Бандери 62, Івано-Франківськ",
            Latitude = 48.9118, Longitude = 24.7129,
            Capacity = 150,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №73 - вул. Тичини",
            Address = "вул. Тичини 149, Івано-Франківськ",
            Latitude = 48.9270, Longitude = 24.6906,
            Capacity = 300,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №74 - вул. Шевченка",
            Address = "вул. Шевченка 113, Івано-Франківськ",
            Latitude = 48.9392, Longitude = 24.6843,
            Capacity = 200,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №75 - вул. Коновальця",
            Address = "вул. Коновальця 12, Івано-Франківськ",
            Latitude = 48.9046, Longitude = 24.7011,
            Capacity = 200,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №76 - вул. Хоткевича",
            Address = "вул. Хоткевича 143, Івано-Франківськ",
            Latitude = 48.9104, Longitude = 24.7212,
            Capacity = 50,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №77 - вул. Незалежності",
            Address = "вул. Незалежності 3, Івано-Франківськ",
            Latitude = 48.9485, Longitude = 24.7212,
            Capacity = 150,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №78 - вул. Галицька",
            Address = "вул. Галицька 116, Івано-Франківськ",
            Latitude = 48.9235, Longitude = 24.6847,
            Capacity = 150,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №79 - вул. Тичини",
            Address = "вул. Тичини 54, Івано-Франківськ",
            Latitude = 48.9083, Longitude = 24.6807,
            Capacity = 100,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №80 - вул. Галицька",
            Address = "вул. Галицька 106, Івано-Франківськ",
            Latitude = 48.9492, Longitude = 24.7002,
            Capacity = 200,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №81 - вул. Миру",
            Address = "вул. Миру 46, Івано-Франківськ",
            Latitude = 48.8972, Longitude = 24.7112,
            Capacity = 100,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №82 - вул. Чорновола",
            Address = "вул. Чорновола 7, Івано-Франківськ",
            Latitude = 48.8908, Longitude = 24.7103,
            Capacity = 500,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №83 - вул. Будівельників",
            Address = "вул. Будівельників 80, Івано-Франківськ",
            Latitude = 48.9196, Longitude = 24.6810,
            Capacity = 500,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №84 - вул. Незалежності",
            Address = "вул. Незалежності 39, Івано-Франківськ",
            Latitude = 48.9373, Longitude = 24.7339,
            Capacity = 150,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №85 - вул. Набережна",
            Address = "вул. Набережна 12, Івано-Франківськ",
            Latitude = 48.8987, Longitude = 24.7292,
            Capacity = 150,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №86 - вул. Незалежності",
            Address = "вул. Незалежності 31, Івано-Франківськ",
            Latitude = 48.8933, Longitude = 24.7209,
            Capacity = 200,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №87 - вул. Тролейбусна",
            Address = "вул. Тролейбусна 60, Івано-Франківськ",
            Latitude = 48.9165, Longitude = 24.6884,
            Capacity = 100,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №88 - вул. Бельведерська",
            Address = "вул. Бельведерська 8, Івано-Франківськ",
            Latitude = 48.9249, Longitude = 24.6801,
            Capacity = 500,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №89 - вул. Галицька",
            Address = "вул. Галицька 106, Івано-Франківськ",
            Latitude = 48.9232, Longitude = 24.6841,
            Capacity = 200,
            Description = "Сховище на території заводу. Надійні перекриття. Розраховане на тривале перебування."
        },
        new()
        {
            Name = "Укриття №90 - вул. Вовчинецька",
            Address = "вул. Вовчинецька 113, Івано-Франківськ",
            Latitude = 48.9004, Longitude = 24.7234,
            Capacity = 500,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №91 - вул. Хоткевича",
            Address = "вул. Хоткевича 33, Івано-Франківськ",
            Latitude = 48.9305, Longitude = 24.7062,
            Capacity = 300,
            Description = "Укриття в дитячому садку. Пристосоване для перебування з дітьми. Є іграшки."
        },
        new()
        {
            Name = "Укриття №92 - вул. Франка",
            Address = "вул. Франка 131, Івано-Франківськ",
            Latitude = 48.9083, Longitude = 24.7077,
            Capacity = 300,
            Description = "Укриття в торговому центрі. Є запаси їжі та води. Працює Wi-Fi."
        },
        new()
        {
            Name = "Укриття №93 - вул. Хоткевича",
            Address = "вул. Хоткевича 119, Івано-Франківськ",
            Latitude = 48.9105, Longitude = 24.7338,
            Capacity = 150,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №94 - вул. Довга",
            Address = "вул. Довга 140, Івано-Франківськ",
            Latitude = 48.8998, Longitude = 24.6903,
            Capacity = 50,
            Description = "Підвал лікарні. Є доступ до медичної допомоги та ліків. Пріоритет для хворих."
        },
        new()
        {
            Name = "Укриття №95 - вул. Сахарова",
            Address = "вул. Сахарова 13, Івано-Франківськ",
            Latitude = 48.9168, Longitude = 24.7344,
            Capacity = 200,
            Description = "Протирадіаційне укриття в адміністративній будівлі. Є генератор та інтернет."
        },
        new()
        {
            Name = "Укриття №96 - вул. Січових Стрільців",
            Address = "вул. Січових Стрільців 89, Івано-Франківськ",
            Latitude = 48.9396, Longitude = 24.7134,
            Capacity = 150,
            Description = "Підземний паркінг новобудови. Сухе та тепле приміщення. Можна з тваринами."
        },
        new()
        {
            Name = "Укриття №97 - вул. Довга",
            Address = "вул. Довга 71, Івано-Франківськ",
            Latitude = 48.9428, Longitude = 24.7296,
            Capacity = 100,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Укриття №98 - вул. Бандери",
            Address = "вул. Бандери 57, Івано-Франківськ",
            Latitude = 48.9319, Longitude = 24.7287,
            Capacity = 300,
            Description = "Підвальне приміщення бібліотеки. Є книги та настільні ігри. Тихо та спокійно."
        },
        new()
        {
            Name = "Укриття №99 - вул. Мазепи",
            Address = "вул. Мазепи 102, Івано-Франківськ",
            Latitude = 48.9243, Longitude = 24.6975,
            Capacity = 100,
            Description = "Підвальне приміщення житлового будинку. Є запас води та місця для сидіння."
        },
        new()
        {
            Name = "Укриття №100 - вул. Тичини",
            Address = "вул. Тичини 129, Івано-Франківськ",
            Latitude = 48.8903, Longitude = 24.7033,
            Capacity = 200,
            Description = "Сховище в університеті. Просторе приміщення. Є лекційні зали, які використовуються як спальні місця."
        },
        new()
        {
            Name = "Shelter Berlin #1",
            Address = "Sonnenallee 49, Berlin",
            Latitude = 52.4711, Longitude = 13.4421,
            Capacity = 400,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #2",
            Address = "Hauptstraße 85, Berlin",
            Latitude = 52.4716, Longitude = 13.3520,
            Capacity = 1000,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #3",
            Address = "Potsdamer Platz 131, Berlin",
            Latitude = 52.5123, Longitude = 13.3336,
            Capacity = 1000,
            Description = "Basement of a public school. Equipped with emergency supplies and first aid kits."
        },
        new()
        {
            Name = "Shelter Berlin #4",
            Address = "Schloßstraße 145, Berlin",
            Latitude = 52.5531, Longitude = 13.3638,
            Capacity = 600,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #5",
            Address = "Potsdamer Platz 164, Berlin",
            Latitude = 52.5694, Longitude = 13.3518,
            Capacity = 600,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #6",
            Address = "Potsdamer Platz 62, Berlin",
            Latitude = 52.4933, Longitude = 13.3831,
            Capacity = 300,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #7",
            Address = "Friedrichstraße 53, Berlin",
            Latitude = 52.4803, Longitude = 13.3650,
            Capacity = 600,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #8",
            Address = "Müllerstraße 60, Berlin",
            Latitude = 52.5697, Longitude = 13.3960,
            Capacity = 100,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #9",
            Address = "Potsdamer Platz 16, Berlin",
            Latitude = 52.5496, Longitude = 13.4031,
            Capacity = 100,
            Description = "Underground parking garage converted for emergency use. Reinforced concrete structure."
        },
        new()
        {
            Name = "Shelter Berlin #10",
            Address = "Sonnenallee 113, Berlin",
            Latitude = 52.5476, Longitude = 13.4154,
            Capacity = 100,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #11",
            Address = "Karl-Marx-Allee 107, Berlin",
            Latitude = 52.4861, Longitude = 13.3403,
            Capacity = 100,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #12",
            Address = "Alexanderplatz 8, Berlin",
            Latitude = 52.5456, Longitude = 13.4414,
            Capacity = 1000,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #13",
            Address = "Schloßstraße 33, Berlin",
            Latitude = 52.5168, Longitude = 13.4108,
            Capacity = 1000,
            Description = "University campus shelter. Large capacity. Internet access and charging stations available."
        },
        new()
        {
            Name = "Shelter Berlin #14",
            Address = "Frankfurter Allee 83, Berlin",
            Latitude = 52.5651, Longitude = 13.4356,
            Capacity = 200,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #15",
            Address = "Karl-Marx-Allee 68, Berlin",
            Latitude = 52.4817, Longitude = 13.3487,
            Capacity = 400,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #16",
            Address = "Sonnenallee 190, Berlin",
            Latitude = 52.5111, Longitude = 13.4030,
            Capacity = 800,
            Description = "Basement of a public school. Equipped with emergency supplies and first aid kits."
        },
        new()
        {
            Name = "Shelter Berlin #17",
            Address = "Schloßstraße 4, Berlin",
            Latitude = 52.4766, Longitude = 13.4036,
            Capacity = 800,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #18",
            Address = "Kurfürstendamm 32, Berlin",
            Latitude = 52.5610, Longitude = 13.4031,
            Capacity = 800,
            Description = "Basement of a public school. Equipped with emergency supplies and first aid kits."
        },
        new()
        {
            Name = "Shelter Berlin #19",
            Address = "Seestraße 174, Berlin",
            Latitude = 52.5318, Longitude = 13.3672,
            Capacity = 1000,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #20",
            Address = "Karl-Marx-Allee 142, Berlin",
            Latitude = 52.4882, Longitude = 13.3338,
            Capacity = 200,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #21",
            Address = "Alexanderplatz 132, Berlin",
            Latitude = 52.5125, Longitude = 13.3316,
            Capacity = 100,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #22",
            Address = "Kurfürstendamm 75, Berlin",
            Latitude = 52.5006, Longitude = 13.3742,
            Capacity = 400,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #23",
            Address = "Kurfürstendamm 138, Berlin",
            Latitude = 52.5455, Longitude = 13.3646,
            Capacity = 100,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #24",
            Address = "Kantstraße 34, Berlin",
            Latitude = 52.5159, Longitude = 13.4590,
            Capacity = 300,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #25",
            Address = "Sonnenallee 128, Berlin",
            Latitude = 52.5076, Longitude = 13.3652,
            Capacity = 1000,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #26",
            Address = "Kantstraße 79, Berlin",
            Latitude = 52.4805, Longitude = 13.3707,
            Capacity = 200,
            Description = "Underground parking garage converted for emergency use. Reinforced concrete structure."
        },
        new()
        {
            Name = "Shelter Berlin #27",
            Address = "Potsdamer Platz 194, Berlin",
            Latitude = 52.4779, Longitude = 13.3598,
            Capacity = 100,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #28",
            Address = "Schloßstraße 133, Berlin",
            Latitude = 52.5202, Longitude = 13.4005,
            Capacity = 100,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #29",
            Address = "Torstraße 197, Berlin",
            Latitude = 52.5623, Longitude = 13.4137,
            Capacity = 600,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #30",
            Address = "Kantstraße 137, Berlin",
            Latitude = 52.5121, Longitude = 13.3528,
            Capacity = 600,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #31",
            Address = "Kantstraße 61, Berlin",
            Latitude = 52.5555, Longitude = 13.4691,
            Capacity = 100,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #32",
            Address = "Hauptstraße 186, Berlin",
            Latitude = 52.5251, Longitude = 13.3318,
            Capacity = 300,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #33",
            Address = "Potsdamer Platz 98, Berlin",
            Latitude = 52.4845, Longitude = 13.4291,
            Capacity = 600,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #34",
            Address = "Torstraße 28, Berlin",
            Latitude = 52.5017, Longitude = 13.4198,
            Capacity = 100,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #35",
            Address = "Oranienstraße 60, Berlin",
            Latitude = 52.5355, Longitude = 13.4094,
            Capacity = 400,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #36",
            Address = "Hermannstraße 154, Berlin",
            Latitude = 52.5665, Longitude = 13.4043,
            Capacity = 800,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #37",
            Address = "Schloßstraße 128, Berlin",
            Latitude = 52.4958, Longitude = 13.3730,
            Capacity = 800,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #38",
            Address = "Seestraße 35, Berlin",
            Latitude = 52.5053, Longitude = 13.4788,
            Capacity = 200,
            Description = "University campus shelter. Large capacity. Internet access and charging stations available."
        },
        new()
        {
            Name = "Shelter Berlin #39",
            Address = "Seestraße 103, Berlin",
            Latitude = 52.5254, Longitude = 13.4237,
            Capacity = 400,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #40",
            Address = "Prenzlauer Allee 78, Berlin",
            Latitude = 52.5407, Longitude = 13.3805,
            Capacity = 600,
            Description = "Underground parking garage converted for emergency use. Reinforced concrete structure."
        },
        new()
        {
            Name = "Shelter Berlin #41",
            Address = "Karl-Marx-Allee 114, Berlin",
            Latitude = 52.5397, Longitude = 13.3343,
            Capacity = 300,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #42",
            Address = "Frankfurter Allee 97, Berlin",
            Latitude = 52.5135, Longitude = 13.4673,
            Capacity = 400,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #43",
            Address = "Bismarckstraße 199, Berlin",
            Latitude = 52.5414, Longitude = 13.3646,
            Capacity = 400,
            Description = "Underground parking garage converted for emergency use. Reinforced concrete structure."
        },
        new()
        {
            Name = "Shelter Berlin #44",
            Address = "Unter den Linden 102, Berlin",
            Latitude = 52.5668, Longitude = 13.4744,
            Capacity = 300,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #45",
            Address = "Schönhauser Allee 180, Berlin",
            Latitude = 52.4984, Longitude = 13.4639,
            Capacity = 400,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #46",
            Address = "Torstraße 43, Berlin",
            Latitude = 52.5171, Longitude = 13.4140,
            Capacity = 200,
            Description = "University campus shelter. Large capacity. Internet access and charging stations available."
        },
        new()
        {
            Name = "Shelter Berlin #47",
            Address = "Torstraße 49, Berlin",
            Latitude = 52.5014, Longitude = 13.3869,
            Capacity = 300,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #48",
            Address = "Hauptstraße 96, Berlin",
            Latitude = 52.5480, Longitude = 13.3652,
            Capacity = 1000,
            Description = "University campus shelter. Large capacity. Internet access and charging stations available."
        },
        new()
        {
            Name = "Shelter Berlin #49",
            Address = "Unter den Linden 141, Berlin",
            Latitude = 52.5565, Longitude = 13.3698,
            Capacity = 200,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #50",
            Address = "Kurfürstendamm 93, Berlin",
            Latitude = 52.5230, Longitude = 13.4450,
            Capacity = 100,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #51",
            Address = "Schloßstraße 67, Berlin",
            Latitude = 52.5098, Longitude = 13.3507,
            Capacity = 400,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #52",
            Address = "Frankfurter Allee 97, Berlin",
            Latitude = 52.5458, Longitude = 13.4229,
            Capacity = 200,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #53",
            Address = "Prenzlauer Allee 123, Berlin",
            Latitude = 52.5111, Longitude = 13.3532,
            Capacity = 1000,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #54",
            Address = "Hermannstraße 130, Berlin",
            Latitude = 52.5112, Longitude = 13.3944,
            Capacity = 600,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #55",
            Address = "Kurfürstendamm 86, Berlin",
            Latitude = 52.5303, Longitude = 13.4757,
            Capacity = 100,
            Description = "Basement of a public school. Equipped with emergency supplies and first aid kits."
        },
        new()
        {
            Name = "Shelter Berlin #56",
            Address = "Kurfürstendamm 188, Berlin",
            Latitude = 52.5240, Longitude = 13.3698,
            Capacity = 800,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #57",
            Address = "Rheinstraße 51, Berlin",
            Latitude = 52.5298, Longitude = 13.4055,
            Capacity = 300,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #58",
            Address = "Müllerstraße 141, Berlin",
            Latitude = 52.5618, Longitude = 13.3893,
            Capacity = 400,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #59",
            Address = "Rheinstraße 42, Berlin",
            Latitude = 52.5013, Longitude = 13.4598,
            Capacity = 100,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #60",
            Address = "Prenzlauer Allee 27, Berlin",
            Latitude = 52.5009, Longitude = 13.4269,
            Capacity = 1000,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #61",
            Address = "Kurfürstendamm 139, Berlin",
            Latitude = 52.5031, Longitude = 13.4575,
            Capacity = 600,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #62",
            Address = "Kantstraße 118, Berlin",
            Latitude = 52.4724, Longitude = 13.3861,
            Capacity = 600,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #63",
            Address = "Schloßstraße 155, Berlin",
            Latitude = 52.5073, Longitude = 13.4571,
            Capacity = 300,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #64",
            Address = "Potsdamer Platz 90, Berlin",
            Latitude = 52.4904, Longitude = 13.4692,
            Capacity = 600,
            Description = "University campus shelter. Large capacity. Internet access and charging stations available."
        },
        new()
        {
            Name = "Shelter Berlin #65",
            Address = "Rheinstraße 151, Berlin",
            Latitude = 52.4889, Longitude = 13.3567,
            Capacity = 300,
            Description = "Basement of a public school. Equipped with emergency supplies and first aid kits."
        },
        new()
        {
            Name = "Shelter Berlin #66",
            Address = "Torstraße 148, Berlin",
            Latitude = 52.5054, Longitude = 13.3397,
            Capacity = 400,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #67",
            Address = "Müllerstraße 164, Berlin",
            Latitude = 52.5282, Longitude = 13.3492,
            Capacity = 800,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #68",
            Address = "Oranienstraße 181, Berlin",
            Latitude = 52.4882, Longitude = 13.4195,
            Capacity = 1000,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #69",
            Address = "Unter den Linden 69, Berlin",
            Latitude = 52.5439, Longitude = 13.3329,
            Capacity = 1000,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #70",
            Address = "Potsdamer Platz 72, Berlin",
            Latitude = 52.4936, Longitude = 13.3592,
            Capacity = 200,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #71",
            Address = "Alexanderplatz 9, Berlin",
            Latitude = 52.4895, Longitude = 13.3315,
            Capacity = 600,
            Description = "Underground parking garage converted for emergency use. Reinforced concrete structure."
        },
        new()
        {
            Name = "Shelter Berlin #72",
            Address = "Torstraße 92, Berlin",
            Latitude = 52.4986, Longitude = 13.4762,
            Capacity = 1000,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #73",
            Address = "Schönhauser Allee 81, Berlin",
            Latitude = 52.5068, Longitude = 13.4067,
            Capacity = 100,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #74",
            Address = "Friedrichstraße 106, Berlin",
            Latitude = 52.4810, Longitude = 13.3867,
            Capacity = 600,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #75",
            Address = "Schloßstraße 53, Berlin",
            Latitude = 52.5386, Longitude = 13.3365,
            Capacity = 1000,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #76",
            Address = "Potsdamer Platz 11, Berlin",
            Latitude = 52.5343, Longitude = 13.3602,
            Capacity = 200,
            Description = "Basement of a public school. Equipped with emergency supplies and first aid kits."
        },
        new()
        {
            Name = "Shelter Berlin #77",
            Address = "Schönhauser Allee 59, Berlin",
            Latitude = 52.5306, Longitude = 13.4169,
            Capacity = 300,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #78",
            Address = "Frankfurter Allee 153, Berlin",
            Latitude = 52.5641, Longitude = 13.3817,
            Capacity = 200,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #79",
            Address = "Kantstraße 153, Berlin",
            Latitude = 52.4730, Longitude = 13.4128,
            Capacity = 100,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #80",
            Address = "Friedrichstraße 46, Berlin",
            Latitude = 52.4808, Longitude = 13.3793,
            Capacity = 1000,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #81",
            Address = "Prenzlauer Allee 99, Berlin",
            Latitude = 52.4745, Longitude = 13.4407,
            Capacity = 1000,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #82",
            Address = "Hermannstraße 193, Berlin",
            Latitude = 52.5633, Longitude = 13.4711,
            Capacity = 1000,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #83",
            Address = "Müllerstraße 20, Berlin",
            Latitude = 52.5529, Longitude = 13.3515,
            Capacity = 100,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #84",
            Address = "Alexanderplatz 172, Berlin",
            Latitude = 52.4824, Longitude = 13.3634,
            Capacity = 600,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #85",
            Address = "Alexanderplatz 132, Berlin",
            Latitude = 52.5319, Longitude = 13.3267,
            Capacity = 600,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #86",
            Address = "Prenzlauer Allee 101, Berlin",
            Latitude = 52.5139, Longitude = 13.4431,
            Capacity = 200,
            Description = "Hospital emergency shelter. Priority for patients and medical staff. Full medical support."
        },
        new()
        {
            Name = "Shelter Berlin #87",
            Address = "Schönhauser Allee 153, Berlin",
            Latitude = 52.4940, Longitude = 13.3303,
            Capacity = 400,
            Description = "Shopping mall underground level. Access to food courts and restrooms. Generator power."
        },
        new()
        {
            Name = "Shelter Berlin #88",
            Address = "Hermannstraße 118, Berlin",
            Latitude = 52.4841, Longitude = 13.3735,
            Capacity = 800,
            Description = "Community center basement. Suitable for families. Water and blankets provided."
        },
        new()
        {
            Name = "Shelter Berlin #89",
            Address = "Kantstraße 82, Berlin",
            Latitude = 52.5470, Longitude = 13.3792,
            Capacity = 200,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #90",
            Address = "Kantstraße 145, Berlin",
            Latitude = 52.5589, Longitude = 13.3902,
            Capacity = 100,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #91",
            Address = "Kurfürstendamm 37, Berlin",
            Latitude = 52.5697, Longitude = 13.4228,
            Capacity = 600,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #92",
            Address = "Potsdamer Platz 199, Berlin",
            Latitude = 52.4806, Longitude = 13.3490,
            Capacity = 1000,
            Description = "Civil defense shelter from the Cold War era. Recently inspected and maintained."
        },
        new()
        {
            Name = "Shelter Berlin #93",
            Address = "Kantstraße 144, Berlin",
            Latitude = 52.5642, Longitude = 13.3864,
            Capacity = 300,
            Description = "University campus shelter. Large capacity. Internet access and charging stations available."
        },
        new()
        {
            Name = "Shelter Berlin #94",
            Address = "Müllerstraße 153, Berlin",
            Latitude = 52.5635, Longitude = 13.3284,
            Capacity = 1000,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #95",
            Address = "Rheinstraße 113, Berlin",
            Latitude = 52.4726, Longitude = 13.4321,
            Capacity = 1000,
            Description = "Basement of a public school. Equipped with emergency supplies and first aid kits."
        },
        new()
        {
            Name = "Shelter Berlin #96",
            Address = "Friedrichstraße 114, Berlin",
            Latitude = 52.5430, Longitude = 13.4678,
            Capacity = 1000,
            Description = "Shelter located in a government building basement. Secure and well-ventilated."
        },
        new()
        {
            Name = "Shelter Berlin #97",
            Address = "Hermannstraße 14, Berlin",
            Latitude = 52.5374, Longitude = 13.3744,
            Capacity = 300,
            Description = "Large multi-purpose bunker. Capable of sustaining occupants for up to 48 hours."
        },
        new()
        {
            Name = "Shelter Berlin #98",
            Address = "Kurfürstendamm 185, Berlin",
            Latitude = 52.5588, Longitude = 13.4390,
            Capacity = 800,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
        new()
        {
            Name = "Shelter Berlin #99",
            Address = "Friedrichstraße 133, Berlin",
            Latitude = 52.5082, Longitude = 13.3321,
            Capacity = 400,
            Description = "Underground parking garage converted for emergency use. Reinforced concrete structure."
        },
        new()
        {
            Name = "Shelter Berlin #100",
            Address = "Kurfürstendamm 109, Berlin",
            Latitude = 52.5601, Longitude = 13.4063,
            Capacity = 1000,
            Description = "Public shelter in a subway station. Accessible during emergencies. Basic amenities available."
        },
    ];
}
