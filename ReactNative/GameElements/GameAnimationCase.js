import React from 'react';
import { StyleSheet , View, Text } from 'react-native';
import { Animated, Easing } from 'react-native';

import 'u/MyConstants';

const styles = StyleSheet.create({
    fieldCase: {
        borderRadius: 4,
        justifyContent: 'center',
        alignItems: 'center',
        margin: CELL__MARGIN,
        borderWidth: 1,
    },
    caseText: {
        fontFamily: 'NerisLight',
        fontSize: 30,
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

export default class UnderlayCase extends React.Component {
    constructor(props) {
        super(props);

        gameCaseSize = (
                SCREEN__WIDTH -
                2*GAME__MARGIN -
                2*CELL__MARGIN*3 - // padding
                gameSize*CELL__MARGIN -
                gameSize // border 1 px
        )/gameSize;


        this.trX = new Animated.Value(0);
        this.trY = new Animated.Value(0);
        this.scaling = new Animated.Value(0);
    }

    shouldComponentUpdate(nextProps) {
        const { translationX, translationY, appearenceAnim } = nextProps;
        const {
            translationX: oldX,
            translationY: oldY,
            appearenceAnim: oldAppearence
        } = this.props;


        const positionChanged = ((translationX !== oldX) || (translationY !== oldY));
        const appearenceChanged = appearenceAnim !== oldAppearence;
        const g = (positionChanged || appearenceChanged);

        if (positionChanged) {
            this.scaling.setValue(0);
            this.trX.setValue(0);
            this.trY.setValue(0);
        }
        if (appearenceChanged && appearenceAnim) {
            this.scaling.setValue(0);
            this.startAppearence();
        }
         return g;
    }
    componentDidUpdate() {
        if ( Math.abs(this.props.translationX) < 1 ) {
            if ( Math.abs(this.props.translationY) < 1 )
                return
            let distance = Math.floor(Math.abs(this.props.translationY)/gameCaseSize);
            Animated.timing(this.trY, {
                toValue: 1000,
                duration: distance*animSpeed,
                useNativeDriver: true,
                easing: Easing.cos
            }).start();
            return
        }
        let distance = Math.floor(Math.abs(this.props.translationX)/gameCaseSize);
        Animated.timing(this.trX, {
            toValue: 1000,
            duration: distance*animSpeed,
            useNativeDriver: true,
            easing: Easing.cos
        }).start();
    }

    startAppearence() {
        Animated.timing(this.scaling, {
            toValue: 1000,
            duration: appearenceSpeed/2,
            useNativeDriver: true,
            easing: Easing.linear
        }).start((finished =>  {
            Animated.timing(this.scaling, {
                toValue: 0,
                duration: appearenceSpeed/2,
                useNativeDriver: true,
                easing: Easing.linear
            }).start();
        }));
    }


    render() {
        const { text, appearenceAnim, translationX, translationY, zIndex, keyIndex  } = this.props;
        var trX = this.trX.interpolate({ inputRange: [0, 1000], outputRange: [0, translationX] });
        var trY = this.trY.interpolate({ inputRange: [0, 1000], outputRange: [0, translationY] });

        let x = (''+text).length;
        let kx_fontSize = (14.1667*Math.pow(x, 3) - 107.5*Math.pow(x, 2) + 328.3333*x - 100)/x;

        var scaleAnim = this.scaling.interpolate({ inputRange: [0, 1000], outputRange: [1, 1.15] });
        //var scaleFont = this.scaling.interpolate({ inputRange: [0, 1000], outputRange: [1, 1.5] });

        return (
            <Animated.View style={[
                styles.fieldCase,
                { backgroundColor: GetColor(text) },
                { opacity: text == 0 ? 0 : 1 },
                { zIndex: zIndex },
                { elevation: zIndex },
                { height: gameCaseSize }, {width: gameCaseSize},
                { transform: [
                    {translateX: text == 0 ? 0 : trX },
                    {translateY: text == 0 ? 0 : trY },
                    { scale: scaleAnim }
                ]},
                { borderColor: text == 0 ? 'transparent' : '#33333355'} ]}>
                <Animated.Text style={[
                    { fontFamily: 'NerisLight' },
                    { fontSize: Math.floor(kx_fontSize/gameSize) }]}>
                { text == 0 ? ' ' : text }
                </Animated.Text>
            </Animated.View>
    )}
}
