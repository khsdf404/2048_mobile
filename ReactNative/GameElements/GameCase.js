import React from 'react';
import { StyleSheet , View, Image, Text } from 'react-native';
import { Animated, Easing } from 'react-native';

import 'u/MyConstants';

const styles = StyleSheet.create({
    fieldCase: {
        borderRadius: 4,
        justifyContent: 'center',
        alignItems: 'center',
        margin: CELL__MARGIN,
        borderWidth: 1
    }
});
function GetColor(cellValue) {
    var colors = [
        '#CCC2B5', // emptyCase
        '#EEE4DA', // Case_2
        '#EDE0C8', // Case_4
        '#F2B179', // Case_8
        '#F59563', // Case_16
        '#F67C5F', // Case_32
        '#F65E3B', // Case_64
        '#EDCE73', // Case_128
        '#E9CE61', // Case_256
        '#EBCA51', // Case_512
        '#EEC63F', // Case_1024
        '#E9C42D' // Case_2048
    ];

    return colors[Math.log2(cellValue == 0 ? 1 : cellValue)];
}

let gameCaseSize;

export default class GameCase extends React.Component {
    constructor(props) {
        super(props);
        gameCaseSize = (
                SCREEN__WIDTH -
                2*GAME__MARGIN -
                2*CELL__MARGIN*3 - // padding
                gameSize*CELL__MARGIN -
                gameSize // border 1 px
        )/gameSize;
    }

    shouldComponentUpdate(nextProps) {
         const { text } = nextProps;
         const { text: oldText } = this.props;

         const textChanged = text !== oldText;

         return textChanged;
    }


    render() {
        const { text, keyIndex  } = this.props;
        let x = (''+text).length;
        let kx_fontSize = (14.1667*Math.pow(x, 3) - 107.5*Math.pow(x, 2) + 328.3333*x - 100)/x;

        return (
        <Animated.View style={[
            styles.fieldCase,
            { backgroundColor: GetColor(text) },
            { height: gameCaseSize }, {width: gameCaseSize},
            { borderColor: text == ' ' ? 'transparent' : '#33333399'}
        ]}>
            <Text style={[
                { fontFamily: 'NerisLight' },
                { fontSize: Math.floor(kx_fontSize/gameSize) }
            ]}> { text == 0 ? ' ' : text } </Text>
        </Animated.View>
    )}
}
