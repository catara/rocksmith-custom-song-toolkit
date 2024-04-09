import * as process from "process";
import * as fs from "fs";
import * as path from "path";
import * as async from "async";
import * as waapi from 'waapi-client';
import * as sass from 'node-sass';
import * as stringify from 'json-stable-stringify';
import { ak } from 'waapi';

var Parser = require("jison").Parser;

const grammar = {
    "lex": {
        "rules": [
            ["\\s+", "/* skip whitespace */"],
            [":", "return ':';"],
            [";", "return ';';"],
            ["exports", "return 'EXPORTS';"],
            ["\\{", "return '{';"],
            ["\\}", "return '}';"],
            ["aliceblue", "return 'aliceblue';"],
            ["antiquewhite", "return 'antiquewhite';"],
            ["aqua", "return 'aqua';"],
            ["aquamarine", "return 'aquamarine';"],
            ["azure", "return 'azure';"],
            ["beige", "return 'beige';"],
            ["bisque", "return 'bisque';"],
            ["black", "return 'black';"],
            ["blanchedalmond", "return 'blanchedalmond';"],
            ["blueviolet", "return 'blueviolet';"],
            ["blue", "return 'blue';"],
            ["brown", "return 'brown';"],
            ["burlywood", "return 'burlywood';"],
            ["cadetblue", "return 'cadetblue';"],
            ["chartreuse", "return 'chartreuse';"],
            ["chocolate", "return 'chocolate';"],
            ["coral", "return 'coral';"],
            ["cornflowerblue", "return 'cornflowerblue';"],
            ["cornsilk", "return 'cornsilk';"],
            ["crimson", "return 'crimson';"],
            ["cyan", "return 'cyan';"],
            ["darkblue", "return 'darkblue';"],
            ["darkcyan", "return 'darkcyan';"],
            ["darkgoldenrod", "return 'darkgoldenrod';"],
            ["darkgray", "return 'darkgray';"],
            ["darkgrey", "return 'darkgrey';"],
            ["darkgreen", "return 'darkgreen';"],
            ["darkkhaki", "return 'darkkhaki';"],
            ["darkmagenta", "return 'darkmagenta';"],
            ["darkolivegreen", "return 'darkolivegreen';"],
            ["darkorange", "return 'darkorange';"],
            ["darkorchid", "return 'darkorchid';"],
            ["darkred", "return 'darkred';"],
            ["darksalmon", "return 'darksalmon';"],
            ["darkseagreen", "return 'darkseagreen';"],
            ["darkslateblue", "return 'darkslateblue';"],
            ["darkslategray", "return 'darkslategray';"],
            ["darkslategrey", "return 'darkslategrey';"],
            ["darkturquoise", "return 'darkturquoise';"],
            ["darkviolet", "return 'darkviolet';"],
            ["deeppink", "return 'deeppink';"],
            ["deepskyblue", "return 'deepskyblue';"],
            ["dimgray", "return 'dimgray';"],
            ["dimgrey", "return 'dimgrey';"],
            ["dodgerblue", "return 'dodgerblue';"],
            ["firebrick", "return 'firebrick';"],
            ["floralwhite", "return 'floralwhite';"],
            ["forestgreen", "return 'forestgreen';"],
            ["fuchsia", "return 'fuchsia';"],
            ["gainsboro", "return 'gainsboro';"],
            ["ghostwhite", "return 'ghostwhite';"],
            ["goldenrod", "return 'goldenrod';"],
            ["gold", "return 'gold';"],
            ["gray", "return 'gray';"],
            ["grey", "return 'grey';"],
            ["greenyellow", "return 'greenyellow';"],
            ["green", "return 'green';"],
            ["honeydew", "return 'honeydew';"],
            ["hotpink", "return 'hotpink';"],
            ["indianred ", "return 'indianred ';"],
            ["indigo ", "return 'indigo ';"],
            ["ivory", "return 'ivory';"],
            ["khaki", "return 'khaki';"],
            ["lavenderblush", "return 'lavenderblush';"],
            ["lavender", "return 'lavender';"],
            ["lawngreen", "return 'lawngreen';"],
            ["lemonchiffon", "return 'lemonchiffon';"],
            ["lightblue", "return 'lightblue';"],
            ["lightcoral", "return 'lightcoral';"],
            ["lightcyan", "return 'lightcyan';"],
            ["lightgoldenrodyellow", "return 'lightgoldenrodyellow';"],
            ["lightgray", "return 'lightgray';"],
            ["lightgrey", "return 'lightgrey';"],
            ["lightgreen", "return 'lightgreen';"],
            ["lightpink", "return 'lightpink';"],
            ["lightsalmon", "return 'lightsalmon';"],
            ["lightseagreen", "return 'lightseagreen';"],
            ["lightskyblue", "return 'lightskyblue';"],
            ["lightslategray", "return 'lightslategray';"],
            ["lightslategrey", "return 'lightslategrey';"],
            ["lightsteelblue", "return 'lightsteelblue';"],
            ["lightyellow", "return 'lightyellow';"],
            ["limegreen", "return 'limegreen';"],
            ["lime", "return 'lime';"],
            ["linen", "return 'linen';"],
            ["magenta", "return 'magenta';"],
            ["maroon", "return 'maroon';"],
            ["mediumaquamarine", "return 'mediumaquamarine';"],
            ["mediumblue", "return 'mediumblue';"],
            ["mediumorchid", "return 'mediumorchid';"],
            ["mediumpurple", "return 'mediumpurple';"],
            ["mediumseagreen", "return 'mediumseagreen';"],
            ["mediumslateblue", "return 'mediumslateblue';"],
            ["mediumspringgreen", "return 'mediumspringgreen';"],
            ["mediumturquoise", "return 'mediumturquoise';"],
            ["mediumvioletred", "return 'mediumvioletred';"],
            ["midnightblue", "return 'midnightblue';"],
            ["mintcream", "return 'mintcream';"],
            ["mistyrose", "return 'mistyrose';"],
            ["moccasin", "return 'moccasin';"],
            ["navajowhite", "return 'navajowhite';"],
            ["navy", "return 'navy';"],
            ["oldlace", "return 'oldlace';"],
            ["olivedrab", "return 'olivedrab';"],
            ["olive", "return 'olive';"],
            ["orangered", "return 'orangered';"],
            ["orange", "return 'orange';"],
            ["orchid", "return 'orchid';"],
            ["palegoldenrod", "return 'palegoldenrod';"],
            ["palegreen", "return 'palegreen';"],
            ["paleturquoise", "return 'paleturquoise';"],
            ["palevioletred", "return 'palevioletred';"],
            ["papayawhip", "return 'papayawhip';"],
            ["peachpuff", "return 'peachpuff';"],
            ["peru", "return 'peru';"],
            ["pink", "return 'pink';"],
            ["plum", "return 'plum';"],
            ["powderblue", "return 'powderblue';"],
            ["purple", "return 'purple';"],
            ["rebeccapurple", "return 'rebeccapurple';"],
            ["red", "return 'red';"],
            ["rosybrown", "return 'rosybrown';"],
            ["royalblue", "return 'royalblue';"],
            ["saddlebrown", "return 'saddlebrown';"],
            ["salmon", "return 'salmon';"],
            ["sandybrown", "return 'sandybrown';"],
            ["seagreen", "return 'seagreen';"],
            ["seashell", "return 'seashell';"],
            ["sienna", "return 'sienna';"],
            ["silver", "return 'silver';"],
            ["skyblue", "return 'skyblue';"],
            ["slateblue", "return 'slateblue';"],
            ["slategray", "return 'slategray';"],
            ["slategrey", "return 'slategrey';"],
            ["snow", "return 'snow';"],
            ["springgreen", "return 'springgreen';"],
            ["steelblue", "return 'steelblue';"],
            ["tan", "return 'tan';"],
            ["teal", "return 'teal';"],
            ["thistle", "return 'thistle';"],
            ["tomato", "return 'tomato';"],
            ["turquoise", "return 'turquoise';"],
            ["violet", "return 'violet';"],
            ["wheat", "return 'wheat';"],
            ["whitesmoke", "return 'whitesmoke';"],
            ["white", "return 'white';"],
            ["yellowgreen", "return 'yellowgreen';"],
            ["yellow", "return 'yellow';"],
            ["^[A-Za-z0-9]+_[A-Za-z0-9]+", "return 'VARIABLE';"],
            ["(^#[a-f0-9A-F]+)|(^rgba\\(([0-9]+),\\s([0-9]+),\\s([0-9]+),\\s((0|1)(\\.?[0-9]*)?)\\))", "return 'TEXT';"],
            ["$", "return 'EOF';"],
        ]
    },

    "operators": [
        ["left", ":"]
    ],

    "bnf": {
        "content": [
            ["EXPORTS { exports } EOF", "return $3;"]
        ],

        "exports": [
            ["exports e", "$$ = [].concat($1, $2);"],
            ["e", "$$ = $1"]
        ],

        "e": [
            ["VARIABLE : aliceblue ;", "$$ = [$1, '#F0F8FF'];"],
            ["VARIABLE : antiquewhite ;", "$$ = [$1, '#FAEBD7'];"],
            ["VARIABLE : aqua ;", "$$ = [$1, '#00FFFF'];"],
            ["VARIABLE : aquamarine ;", "$$ = [$1, '#7FFFD4'];"],
            ["VARIABLE : azure ;", "$$ = [$1, '#F0FFFF'];"],
            ["VARIABLE : beige ;", "$$ = [$1, '#F5F5DC'];"],
            ["VARIABLE : bisque ;", "$$ = [$1, '#FFE4C4'];"],
            ["VARIABLE : black ;", "$$ = [$1, '#000000'];"],
            ["VARIABLE : blanchedalmond ;", "$$ = [$1, '#FFEBCD'];"],
            ["VARIABLE : blue ;", "$$ = [$1, '#0000FF'];"],
            ["VARIABLE : blueviolet ;", "$$ = [$1, '#8A2BE2'];"],
            ["VARIABLE : brown ;", "$$ = [$1, '#A52A2A'];"],
            ["VARIABLE : burlywood ;", "$$ = [$1, '#DEB887'];"],
            ["VARIABLE : cadetblue ;", "$$ = [$1, '#5F9EA0'];"],
            ["VARIABLE : chartreuse ;", "$$ = [$1, '#7FFF00'];"],
            ["VARIABLE : chocolate ;", "$$ = [$1, '#D2691E'];"],
            ["VARIABLE : coral ;", "$$ = [$1, '#FF7F50'];"],
            ["VARIABLE : cornflowerblue ;", "$$ = [$1, '#6495ED'];"],
            ["VARIABLE : cornsilk ;", "$$ = [$1, '#FFF8DC'];"],
            ["VARIABLE : crimson ;", "$$ = [$1, '#DC143C'];"],
            ["VARIABLE : cyan ;", "$$ = [$1, '#00FFFF'];"],
            ["VARIABLE : darkblue ;", "$$ = [$1, '#00008B'];"],
            ["VARIABLE : darkcyan ;", "$$ = [$1, '#008B8B'];"],
            ["VARIABLE : darkgoldenrod ;", "$$ = [$1, '#B8860B'];"],
            ["VARIABLE : darkgray ;", "$$ = [$1, '#A9A9A9'];"],
            ["VARIABLE : darkgrey ;", "$$ = [$1, '#A9A9A9'];"],
            ["VARIABLE : darkgreen ;", "$$ = [$1, '#006400'];"],
            ["VARIABLE : darkkhaki ;", "$$ = [$1, '#BDB76B'];"],
            ["VARIABLE : darkmagenta ;", "$$ = [$1, '#8B008B'];"],
            ["VARIABLE : darkolivegreen ;", "$$ = [$1, '#556B2F'];"],
            ["VARIABLE : darkorange ;", "$$ = [$1, '#FF8C00'];"],
            ["VARIABLE : darkorchid ;", "$$ = [$1, '#9932CC'];"],
            ["VARIABLE : darkred ;", "$$ = [$1, '#8B0000'];"],
            ["VARIABLE : darksalmon ;", "$$ = [$1, '#E9967A'];"],
            ["VARIABLE : darkseagreen ;", "$$ = [$1, '#8FBC8F'];"],
            ["VARIABLE : darkslateblue ;", "$$ = [$1, '#483D8B'];"],
            ["VARIABLE : darkslategray ;", "$$ = [$1, '#2F4F4F'];"],
            ["VARIABLE : darkslategrey ;", "$$ = [$1, '#2F4F4F'];"],
            ["VARIABLE : darkturquoise ;", "$$ = [$1, '#00CED1'];"],
            ["VARIABLE : darkviolet ;", "$$ = [$1, '#9400D3'];"],
            ["VARIABLE : deeppink ;", "$$ = [$1, '#FF1493'];"],
            ["VARIABLE : deepskyblue ;", "$$ = [$1, '#00BFFF'];"],
            ["VARIABLE : dimgray ;", "$$ = [$1, '#696969'];"],
            ["VARIABLE : dimgrey ;", "$$ = [$1, '#696969'];"],
            ["VARIABLE : dodgerblue ;", "$$ = [$1, '#1E90FF'];"],
            ["VARIABLE : firebrick ;", "$$ = [$1, '#B22222'];"],
            ["VARIABLE : floralwhite ;", "$$ = [$1, '#FFFAF0'];"],
            ["VARIABLE : forestgreen ;", "$$ = [$1, '#228B22'];"],
            ["VARIABLE : fuchsia ;", "$$ = [$1, '#FF00FF'];"],
            ["VARIABLE : gainsboro ;", "$$ = [$1, '#DCDCDC'];"],
            ["VARIABLE : ghostwhit e;", "$$ = [$1, '#F8F8FF'];"],
            ["VARIABLE : gold ;", "$$ = [$1, '#FFD700'];"],
            ["VARIABLE : goldenrod ;", "$$ = [$1, '#DAA520'];"],
            ["VARIABLE : gray ;", "$$ = [$1, '#808080'];"],
            ["VARIABLE : grey ;", "$$ = [$1, '#808080'];"],
            ["VARIABLE : green ;", "$$ = [$1, '#008000'];"],
            ["VARIABLE : greenyellow ;", "$$ = [$1, '#ADFF2F'];"],
            ["VARIABLE : honeydew ;", "$$ = [$1, '#F0FFF0'];"],
            ["VARIABLE : hotpink ;", "$$ = [$1, '#FF69B4'];"],
            ["VARIABLE : indianred ;", "$$ = [$1, '#CD5C5C'];"],
            ["VARIABLE : indigo ;", "$$ = [$1, '#4B0082'];"],
            ["VARIABLE : ivory ;", "$$ = [$1, '#FFFFF0'];"],
            ["VARIABLE : khaki ;", "$$ = [$1, '#F0E68C'];"],
            ["VARIABLE : lavender ;", "$$ = [$1, '#E6E6FA'];"],
            ["VARIABLE : lavenderblush ;", "$$ = [$1, '#FFF0F5'];"],
            ["VARIABLE : lawngreen ;", "$$ = [$1, '#7CFC00'];"],
            ["VARIABLE : lemonchiffon ;", "$$ = [$1, '#FFFACD'];"],
            ["VARIABLE : lightblue ;", "$$ = [$1, '#ADD8E6'];"],
            ["VARIABLE : lightcoral ;", "$$ = [$1, '#F08080'];"],
            ["VARIABLE : lightcyan ;", "$$ = [$1, '#E0FFFF'];"],
            ["VARIABLE : lightgoldenrodyellow ;", "$$ = [$1, '#FAFAD2'];"],
            ["VARIABLE : lightgray ;", "$$ = [$1, '#D3D3D3'];"],
            ["VARIABLE : lightgrey ;", "$$ = [$1, '#D3D3D3'];"],
            ["VARIABLE : lightgreen ;", "$$ = [$1, '#90EE90'];"],
            ["VARIABLE : lightpink ;", "$$ = [$1, '#FFB6C1'];"],
            ["VARIABLE : lightsalmon ;", "$$ = [$1, '#FFA07A'];"],
            ["VARIABLE : lightseagreen ;", "$$ = [$1, '#20B2AA'];"],
            ["VARIABLE : lightskyblue ;", "$$ = [$1, '#87CEFA'];"],
            ["VARIABLE : lightslategray ;", "$$ = [$1, '#778899'];"],
            ["VARIABLE : lightslategrey ;", "$$ = [$1, '#778899'];"],
            ["VARIABLE : lightsteelblue ;", "$$ = [$1, '#B0C4DE'];"],
            ["VARIABLE : lightyellow ;", "$$ = [$1, '#FFFFE0'];"],
            ["VARIABLE : lime ;", "$$ = [$1, '#00FF00'];"],
            ["VARIABLE : limegreen ;", "$$ = [$1, '#32CD32'];"],
            ["VARIABLE : linen ;", "$$ = [$1, '#FAF0E6'];"],
            ["VARIABLE : magenta ;", "$$ = [$1, '#FF00FF'];"],
            ["VARIABLE : maroon ;", "$$ = [$1, '#800000'];"],
            ["VARIABLE : mediumaquamarine ;", "$$ = [$1, '#66CDAA'];"],
            ["VARIABLE : mediumblue ;", "$$ = [$1, '#0000CD'];"],
            ["VARIABLE : mediumorchid ;", "$$ = [$1, '#BA55D3'];"],
            ["VARIABLE : mediumpurple ;", "$$ = [$1, '#9370DB'];"],
            ["VARIABLE : mediumseagreen ;", "$$ = [$1, '#3CB371'];"],
            ["VARIABLE : mediumslateblue ;", "$$ = [$1, '#7B68EE'];"],
            ["VARIABLE : mediumspringgreen ;", "$$ = [$1, '#00FA9A'];"],
            ["VARIABLE : mediumturquoise ;", "$$ = [$1, '#48D1CC'];"],
            ["VARIABLE : mediumvioletred ;", "$$ = [$1, '#C71585'];"],
            ["VARIABLE : midnightblue ;", "$$ = [$1, '#191970'];"],
            ["VARIABLE : mintcream ;", "$$ = [$1, '#F5FFFA'];"],
            ["VARIABLE : mistyrose ;", "$$ = [$1, '#FFE4E1'];"],
            ["VARIABLE : moccasin ;", "$$ = [$1, '#FFE4B5'];"],
            ["VARIABLE : navajowhite ;", "$$ = [$1, '#FFDEAD'];"],
            ["VARIABLE : navy ;", "$$ = [$1, '#000080'];"],
            ["VARIABLE : oldlace ;", "$$ = [$1, '#FDF5E6'];"],
            ["VARIABLE : olive ;", "$$ = [$1, '#808000'];"],
            ["VARIABLE : olivedrab ;", "$$ = [$1, '#6B8E23'];"],
            ["VARIABLE : orange ;", "$$ = [$1, '#FFA500'];"],
            ["VARIABLE : orangered ;", "$$ = [$1, '#FF4500'];"],
            ["VARIABLE : orchid ;", "$$ = [$1, '#DA70D6'];"],
            ["VARIABLE : palegoldenrod ;", "$$ = [$1, '#EEE8AA'];"],
            ["VARIABLE : palegreen ;", "$$ = [$1, '#98FB98'];"],
            ["VARIABLE : paleturquoise ;", "$$ = [$1, '#AFEEEE'];"],
            ["VARIABLE : palevioletred ;", "$$ = [$1, '#DB7093'];"],
            ["VARIABLE : papayawhip ;", "$$ = [$1, '#FFEFD5'];"],
            ["VARIABLE : peachpuff ;", "$$ = [$1, '#FFDAB9'];"],
            ["VARIABLE : peru ;", "$$ = [$1, '#CD853F'];"],
            ["VARIABLE : pink ;", "$$ = [$1, '#FFC0CB'];"],
            ["VARIABLE : plum ;", "$$ = [$1, '#DDA0DD'];"],
            ["VARIABLE : powderblue ;", "$$ = [$1, '#B0E0E6'];"],
            ["VARIABLE : purple ;", "$$ = [$1, '#800080'];"],
            ["VARIABLE : rebeccapurple ;", "$$ = [$1, '#663399'];"],
            ["VARIABLE : red ;", "$$ = [$1, '#FF0000'];"],
            ["VARIABLE : rosybrown ;", "$$ = [$1, '#BC8F8F'];"],
            ["VARIABLE : royalblue ;", "$$ = [$1, '#4169E1'];"],
            ["VARIABLE : saddlebrown ;", "$$ = [$1, '#8B4513'];"],
            ["VARIABLE : salmon ;", "$$ = [$1, '#FA8072'];"],
            ["VARIABLE : sandybrown ;", "$$ = [$1, '#F4A460'];"],
            ["VARIABLE : seagreen ;", "$$ = [$1, '#2E8B57'];"],
            ["VARIABLE : seashell ;", "$$ = [$1, '#FFF5EE'];"],
            ["VARIABLE : sienna ;", "$$ = [$1, '#A0522D'];"],
            ["VARIABLE : silver ;", "$$ = [$1, '#C0C0C0'];"],
            ["VARIABLE : skyblue ;", "$$ = [$1, '#87CEEB'];"],
            ["VARIABLE : slateblue ;", "$$ = [$1, '#6A5ACD'];"],
            ["VARIABLE : slategray ;", "$$ = [$1, '#708090'];"],
            ["VARIABLE : slategrey ;", "$$ = [$1, '#708090'];"],
            ["VARIABLE : snow ;", "$$ = [$1, '#FFFAFA'];"],
            ["VARIABLE : springgreen ;", "$$ = [$1, '#00FF7F'];"],
            ["VARIABLE : steelblue ;", "$$ = [$1, '#4682B4'];"],
            ["VARIABLE : tan ;", "$$ = [$1, '#D2B48C'];"],
            ["VARIABLE : teal ;", "$$ = [$1, '#008080'];"],
            ["VARIABLE : thistle ;", "$$ = [$1, '#D8BFD8'];"],
            ["VARIABLE : tomato ;", "$$ = [$1, '#FF6347'];"],
            ["VARIABLE : turquoise ;", "$$ = [$1, '#40E0D0'];"],
            ["VARIABLE : violet ;", "$$ = [$1, '#EE82EE'];"],
            ["VARIABLE : wheat ;", "$$ = [$1, '#F5DEB3'];"],
            ["VARIABLE : white ;", "$$ = [$1, '#FFFFFF'];"],
            ["VARIABLE : whitesmoke ;", "$$ = [$1, '#F5F5F5'];"],
            ["VARIABLE : yellow ;", "$$ = [$1, '#FFFF00'];"],
            ["VARIABLE : yellowgreen ;", "$$ = [$1, '#9ACD32'];"],
            ["VARIABLE : TEXT ;", "$$ = [$1, $3];"],
            ["VARIABLE", "$$ = yytext;"],
            ["TEXT", "$$ = yytext;"]
        ]
    }
};

const inputSassFile = "main.scss";
const outputJsonFile = "main.json";

let parser = new Parser(grammar);

console.log();

fs.readdir(".", (err: Error, files: string[]) => {

    async.each(files, (filename: string, callback) => {
        fs.lstat(filename, (err: Error, stats: fs.Stats) => {

            if (stats.isDirectory()) {
                compileSass(filename, (err: Error) => {
                    return callback(err);
                });
            } else {
                return callback();
            }
        });
    }, async (err: Error) => {

        if (err) {
            console.error(err);
            process.exit(1);
        }

        await refreshWwise();        

        console.log("Job's done!");
        process.exit(0);
    });
});

function convertFromHex(color: string): string {
    
    if (color[0] != "#") {
        return color;
    }

    const r = parseInt(color.substr(1, 2), 16);
    const g = parseInt(color.substr(3, 2), 16);
    const b = parseInt(color.substr(5, 2), 16);

    return `rgba(${r}, ${g}, ${b}, 1)`;
}

function compileSass(folder: string, callback: (err?: Error) => void) {

    const inputFile: string = path.join(folder, inputSassFile);

    // Silent skip if input file doesn't exist, we don't want to fail on unrelated folders.
    try {
        fs.accessSync(inputFile);
    } catch (e) {
        return callback();
    }

    console.log(`Compiling ${inputFile}`);

    sass.render({
        file: inputFile,
    }, (err, result) => {

        if (err) {
            return callback(err);
        }

        let cssString: string = String(result.css);
        let parseResult: any = null;

        try {
            parseResult = parser.parse(cssString);
        } catch (e) {
            return callback(e);
        }

        let jsonObject = {};

        for (let i: number = 0; i < parseResult.length; ++i) {

            if (jsonObject.hasOwnProperty(parseResult[i])) {
                console.warn(`Conflict detected in ${inputFile}: ${parseResult[i]}`)
            }

            jsonObject[parseResult[i]] = convertFromHex(parseResult[++i]);
        }

        const outputFile: string = path.join(folder, outputJsonFile);

        fs.writeFile(outputFile, stringify(jsonObject, { space: 4 }), 'utf-8', (err) => {

            if (err) {
                return callback(err);
            }

            return callback();
        });
    });
}


async function refreshWwise() {

    try {
        // Connect to WAAPI
        // Ensure you enabled WAAPI in Wwise's User Preferences. 
        // Refer to readme.md for more information
        var session = await waapi.connect('ws://localhost:8080/waapi');

        var wwiseInfo = await session.call(ak.wwise.ui.commands.execute, {command:"ReloadCurrentSkin"});
        await session.disconnect();
        console.log("Wwise updated");
    }
    catch (e) {
        if(e.message)
            console.log(`Could not update Wwise: '${e.message}'`);
        else
            console.log(`Could not update Wwise: ${JSON.stringify(e,null,4)}`);
    }    
}