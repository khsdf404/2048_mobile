import React from 'react';
import { StyleSheet , View, Image, Animated, Text, Button  } from 'react-native';

import 'u/MyConstants';
let rotationDegrees = ['-7deg', '4deg', '-1deg', '2deg'];
let edge = 5;
let flySpeed = 1500;


const styles = StyleSheet.create({
    logoWrapp: {
        justifyContent: 'center',
        alignItems: 'center',
        flexDirection: 'row'
    },
    logoSquare: {
        justifyContent: 'center',
        alignItems: 'center',
        flexDirection: 'row',
        marginLeft: 1,
        height: SCREEN__WIDTH/4.3,
        width: SCREEN__WIDTH/4.3,
        borderRadius: 4,
        borderWidth: 1,
        borderColor: '#000'

    },
    logoSquareText: {
        fontSize: 40,
        fontFamily: 'NerisLight'
    }
});


export default class FlyingSquares extends React.Component {
    constructor() {
        super()
        this.Translation_2 = new Animated.Value(0);
        this.Translation_0 = new Animated.Value(0);
        this.Translation_4 = new Animated.Value(0);
        this.Translation_8 = new Animated.Value(0);
    }
    animateStart(value, delay) {
        Animated.timing(value, {
          toValue: 0,
          duration: delay,
          useNativeDriver: false
        }).start(({ finished }) => {
            this.animateTo(value);
        });
    }
    animateTo(value) {
        Animated.timing(value, {
          toValue: edge,
          duration: flySpeed,
          useNativeDriver: false
        }).start(({ finished }) => {
            this.animateBack(value);
        });
    }
    animateBack(value) {
        Animated.timing(value, {
          toValue: -1*edge,
          duration: flySpeed,
          useNativeDriver: false
        }).start(({ finished }) => {
            this.animateTo(value);
        });
    }

    shouldComponentUpdate(nextProps){
        return false;
    }

    render() {
        return (
            <View  style={styles.logoWrapp}>
                <Animated.View onPress={this.animateStart(this.Translation_2, 0)} style={{transform: [{translateY: this.Translation_2}]}}>
                    <View style={[styles.logoSquare, {backgroundColor: '#EEE4DA'}, { transform: [{ rotate: rotationDegrees[0] }] }]}>
                        <Text style={styles.logoSquareText}>2</Text>
                    </View>
                </Animated.View>

                <Animated.View onPress={this.animateStart(this.Translation_0, 700)} style={{transform: [{translateY: this.Translation_0}]}}>
                    <View style={[styles.logoSquare, {backgroundColor: '#CCC2B5'}, { transform: [{ rotate: rotationDegrees[1] }] }]}>
                        <Text style={styles.logoSquareText}>0</Text>
                    </View>
                </Animated.View>

                <Animated.View onPress={this.animateStart(this.Translation_4, 1850)} style={{transform: [{translateY: this.Translation_4 }]}}>
                    <View style={[styles.logoSquare, {backgroundColor: '#EDE0C8'}, { transform: [{ rotate: rotationDegrees[2] }] }]}>
                        <Text style={styles.logoSquareText}>4</Text>
                    </View>
                </Animated.View>

                <Animated.View onPress={this.animateStart(this.Translation_8, 300)} style={{transform: [{translateY: this.Translation_8}]}}>
                    <View style={[styles.logoSquare, {backgroundColor: '#F2B179'}, { transform: [{ rotate: rotationDegrees[3] }] }]}>
                        <Text style={styles.logoSquareText}>8</Text>
                    </View>
                </Animated.View>
            </View>
        )
    }
}
