import React from 'react';
import { View, Image, TouchableWithoutFeedback, StyleSheet  } from 'react-native';

import 'u/MyConstants';

const styles = StyleSheet.create({
    rightArea: {
        alignItems: 'baseline',
        justifyContent: 'flex-end',
        alignItems: 'flex-end',
    },
    navIconsRow: {
        justifyContent: 'flex-end',
        alignItems: 'flex-end',
    },
    navIcon: {
        width: 35,
        height: 35,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#444444CC',
        borderRadius: 2,
        marginHorizontal: 1
    },
    Icon: {
        position: 'absolute',
        width: 35,
        height: 35
    },
});


export default class NavButtons extends React.Component {
    constructor(props) {
        super(props);
    }
    shouldComponentUpdate(nextProps) {
         return false;
    }
    render() {
        return (
            <View style={styles.rightArea}>
                <View style={styles.navIconsRow}>
                    <TouchableWithoutFeedback onPress={ () => this.props.homePress() }>
                        <View style={styles.navIcon}>
                            <Image style={styles.Icon} source={require('u/assets/house.png')}/>
                        </View>
                    </TouchableWithoutFeedback>
                </View>
                <View style={[styles.navIconsRow, {marginTop: 2}, {flexDirection: 'row'}]}>
                    <TouchableWithoutFeedback onPress={ () => this.props.stepPress() }>
                        <View style={styles.navIcon}>
                            <Image
                                style={[styles.Icon, {transform: [{scale: 0.7}]}]}
                                source={require('u/assets/left.png')}/>
                        </View>
                    </TouchableWithoutFeedback>
                    <TouchableWithoutFeedback onPress={ () => this.props.resetPress() }>
                        <View style={styles.navIcon}>
                            <Image
                                style={[styles.Icon, {transform: [{scale: 0.7}]}]}
                                source={require('u/assets/sync.png')}/>
                        </View>
                    </TouchableWithoutFeedback>
                </View>
            </View>
        )
    }
}
