import React from 'react';
import { Dimensions, StatusBar } from 'react-native'



global.SCREEN__WIDTH = Dimensions.get('window').width;
global.SCREEN__HEIGHT = Dimensions.get('window').height;
global.CONST__MT = 0.2*SCREEN__HEIGHT
global.BAR__HEIGHT = StatusBar.currentHeight;
global.GAME__MARGIN = 0.02*SCREEN__WIDTH;
global.CELL__MARGIN = 0.003*SCREEN__WIDTH;

global.SETTINGS__WIDTH = 0.8*SCREEN__WIDTH
global.SETTINGS__PADDING = 0.03*SCREEN__WIDTH;
global.SETTINGS__GRIDMARGIN = 0.005*SCREEN__WIDTH;
global.SETTINGS__MT = 0.015*SCREEN__HEIGHT;

global.gameSize = 4;
global.animSpeed = 900;
global.appearenceSpeed = 150;
global.appearenceAllowed = true;
