'use strict';
const electron = require('electron');
// Module to control application life.
const app = electron.app;
// Module to create native browser window.
const BrowserWindow = electron.BrowserWindow;
const globalShortcut = electron.globalShortcut

const path = require('path');
const url = require('url');

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let mainWindow;
let splashScreen;

function createWindow() {
    // Create the browser window.
    mainWindow = new BrowserWindow({
        //icon: path.join(__dirname, './/assets//icons/icon.png')
        webPreferences: {
            plugins: true
        }
    });

    // and load the index.html of the app.
    mainWindow.loadURL(url.format({
        pathname: 'localhost:8989/', //path.join(__dirname, 'index.html'),
        protocol: 'http:',
        slashes: true
    }));

    // Open the DevTools.
    //mainWindow.webContents.openDevTools();

    // Emitted when the window is closed.
    mainWindow.on('closed', function () {
        // Dereference the window object, usually you would store windows
        // in an array if your app supports multi windows, this is the time
        // when you should delete the corresponding element.
        mainWindow = null;
    });
    
    globalShortcut.register('CommandOrControl+R', function () {
        mainWindow.reload()
    });

    // Start with full screen.
    mainWindow.maximize();
}

function createSplashScreen() {
    // Create the browser window.
    splashScreen = new BrowserWindow({
        //icon: path.join(__dirname, './/assets//icons/icon.png')
        autoHideMenuBar: true
    });

    // and load the index.html of the app.
    //splashScreen.loadURL(url.format({
    //    pathname: 'localhost:8989/', //path.join(__dirname, 'index.html'),
    //    protocol: 'http:',
    //    slashes: true
    //}));

    // Open the DevTools.
    //mainWindow.webContents.openDevTools();

    // Emitted when the window is closed.
    splashScreen.on('closed', function () {
        // Dereference the window object, usually you would store windows
        // in an array if your app supports multi windows, this is the time
        // when you should delete the corresponding element.
        splashScreen = null;
    });
}

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.on('ready', () => {
    createSplashScreen();
    startApi();
});

// Quit when all windows are closed.
app.on('window-all-closed', function () {
    // On OS X it is common for applications and their menu bar
    // to stay active until the user quits explicitly with Cmd + Q
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

app.on('activate', function () {
    // On OS X it's common to re-create a window in the app when the
    // dock icon is clicked and there are no other windows open.
    if (mainWindow == null) {
        createWindow();
    }
});
// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and require them here.

const os = require('os');
var apiProcess = null;

function startApi() {
    var proc = require('child_process').spawn;
    //  run server
    var apipath = path.join(__dirname, '..\\server\\Estimation.WebApi.exe');
    var options = {
        cwd: path.join(__dirname, '..\\server'),
        env: process.env
    };
    if (os.platform() === 'darwin') {
        apipath = path.join(__dirname, '..//server//osx//MiniWing');
        options.cwd = path.join(__dirname, '..//server//osx');
    }

    apiProcess = proc(apipath, [], options);

    apiProcess.stdout.on('data', (data) => {
        writeLog(`stdout: ${data}`);
        
        if (mainWindow == null) {
            createWindow();
        }
        if (splashScreen != null) {
            splashScreen.close();
        }
    });
}

//Kill process when electron exits
process.on('exit', function () {
    writeLog('exit');
    if (apiProcess)
        apiProcess.kill('SIGSTOP');
});

function writeLog(msg) {
    console.log(msg);
}