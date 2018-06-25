const fs = require('fs');
const path = require('path');

const settingsFileName = 'settings.json';
const htmlFileName = 'index.html';

const processChange = (directory) => {

  const settings = fs.readFileSync(path.join(directory, settingsFileName)).toString();
  const htmlContent = fs.readFileSync(path.join(directory, htmlFileName)).toString();

  const newHtmlContent = htmlContent.replace(/\/\*\s*<inject-settings>\s*\*\/[\s\S]*\/\*\s*<\/inject-settings>\s*\*\//g,
    `/*<inject-settings>*/\nwindow.__injected_settings__ =  ${settings}\n/*</inject-settings>*/`);

    if (htmlContent !== newHtmlContent) {
        fs.writeFileSync(path.join(directory, htmlFileName), newHtmlContent);
        console.log(`Processed directory ${directory}.`);
    }
};

const dir = '/app/dist';

try {
  processChange(dir);
} catch (e){
  console.error(e);
}

fs.watch(dir, {encoding: 'buffer'}, (eventType, filename) => {

    let name = filename.toString();

    if (name === settingsFileName || name === htmlFileName) {
      try {
        processChange(dir);
      } catch (e){
        console.error(e);
      }
    }
});

console.log('Inject-Settings is now running.');
