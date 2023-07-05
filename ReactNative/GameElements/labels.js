import React from 'react';
import { StyleSheet, View, Text } from 'react-native';

import 'u/MyConstants';

const styles = StyleSheet.create({
    leftArea: {
        alignItems: 'baseline',
    },
    navTextWrap: {
        padding: 6,
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 3,
        borderWidth: 1,
        borderColor: '#33333355',
        backgroundColor: '#EEE4DA',
    },
    navText: {
        fontSize: 16,
        fontFamily: 'NerisBold'
    },
});


export default class NavLabels extends React.Component {
    constructor(props) {
        super(props);
    }

    shouldComponentUpdate(nextProps) {
         const { currScore, currTime } = nextProps;
         const { currScore: oldScore, currTime: oldTime } = this.props;

         const scoreChanged = currScore !== oldScore;
         const timeChanged = currTime !== oldTime;

         const shouldChange = (scoreChanged||timeChanged);

         return shouldChange;
    }

    render() {
        const { currScore, currTime  } = this.props;
        return (
            <View style={styles.leftArea}>
                <View style={[styles.navTextWrap, {width: 0.75*16*(currTime.length) }]}>
                    <Text style={styles.navText}>{currTime}</Text>
                </View>
                <View style={[styles.navTextWrap, {marginTop: 1}]}>
                    <Text style={styles.navText}>Score: {currScore}</Text>
                </View>
            </View>
        )
    }
}
