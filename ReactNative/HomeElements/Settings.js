import React, { useState } from 'react';
import { StyleSheet , View, Pressable, Image, Text, TouchableWithoutFeedback, FlatList } from 'react-native';
import { Animated, Easing } from 'react-native';

import 'u/MyConstants';

import GridCell from 'u/HomeElements/GridCell'
import ProgressBar from 'u/HomeElements/ProgressBar'
import AppearenceSwitch from 'u/HomeElements/AppearenceSwitch'

let gridDATA = [];

const styles = StyleSheet.create({
    settingsVeil: {
        height: SCREEN__HEIGHT,
        width: SCREEN__WIDTH,
        top: BAR__HEIGHT,
        position: 'absolute',
        alignItems: 'flex-end',
    },
    settingsWrap: {
        height: SCREEN__HEIGHT,
        width: SCREEN__WIDTH*(80/100),
        //position: 'absolute',
        backgroundColor: '#fff',
        borderColor: '#000',
        borderWidth: 1,
        borderRightWidth: 0,
        borderBottomLeftRadius: 10,
        borderTopLeftRadius: 10,
        justifyContent: 'center',
        alignItems: 'center',
        padding: SCREEN__WIDTH*(3/100)
    },
});



export default class Settings extends React.Component {
    constructor(props) {
        super(props);
        this.settingsTransAnim = new Animated.Value(0);
        this.settingsColorAnim = new Animated.Value(0);
    }
    state = {
        zIndex: -1,
        gridDATA: gridDATA,
        selectedCell: gameSize,
        wereOpened: false
    }

    componentDidMount() {
        if (gridDATA.length > 0)
            return
        for(let i = 0; i < 6; i ++) {
            gridDATA.push({
                selected: i+3 == gameSize ? true : false,
                textUnit: (i+3),
                keyId: 'gridCell_'+(i+3)
            })
        }
    }
    async shouldComponentUpdate(nextProps) {
        const { opened } = nextProps;
        const { opened: oldProp } = this.props;

        const wereClicked = opened !== oldProp;

        if (wereClicked) {
            this.showSettings();
            await this.setState({ zIndex: 1, wereOpened: true });
        }

        return wereClicked;
    }

    showSettings() {
        this.settingsTransAnim.setValue(0);
        this.settingsColorAnim.setValue(0);
        Animated.timing(this.settingsTransAnim, {
            toValue: 1000,
            duration: 400,
            useNativeDriver: true
        }).start((finished => {

        }));
        Animated.timing(this.settingsColorAnim, {
            toValue: 1000,
            duration: 200,
            useNativeDriver: false
        }).start();
    }
    hideSettings(e) {
        if (e.nativeEvent.pageX < SCREEN__WIDTH*0.2) {
            this.settingsTransAnim.setValue(1000);
            this.settingsColorAnim.setValue(1000);
            Animated.timing(this.settingsTransAnim, {
                toValue: 0,
                duration: 200,
                useNativeDriver: true
            }).start();
            Animated.timing(this.settingsColorAnim, {
                toValue: 0,
                duration: 400,
                useNativeDriver: false
            }).start(( finished => {
                this.setState({zIndex: -1, wereOpened: false});
            }));
        }
    }

    gridRenderOption = (item) => {
        return (
            <GridCell
                selected = { item.selected }
                textUnit = { item.textUnit }
                GridCellClick={ () =>  this.GridCellClick(item.textUnit) }
            />
        );
    }
    GridCellClick(textUnit) {
        gridDATA[gameSize-3].selected = false
        gridDATA[parseInt(textUnit) - 3].selected = true
        gameSize = parseInt(textUnit);
        this.setState({ gridDATA: gridDATA });
    }

    render() {
        const { opened } = this.props

        var settingsLeft = this.settingsTransAnim.interpolate({ inputRange: [0, 1000], outputRange: [SCREEN__WIDTH*0.8, 0] });
        var settingsColor = this.settingsColorAnim.interpolate({ inputRange: [0, 1000], outputRange: ['#00000000', '#000000AA'] });


        return (
            <TouchableWithoutFeedback onPress={(e) => this.hideSettings(e) } >
                <Animated.View style={[styles.settingsVeil, {zIndex: this.state.zIndex}, {backgroundColor: settingsColor} ]}>
                    <Animated.View style={[styles.settingsWrap, {transform: [{ translateX: settingsLeft }]}]}>
                        <View>
                            <View style={[
                                {padding: SETTINGS__PADDING/3},
                                {borderWidth: 1},
                                {borderRadius: 4}
                            ]}>
                                <FlatList
                                    key={'settingsGrid3'}
                                    numColumns={3}
                                    style={[{flexGrow: 0}]}
                                    data={this.state.gridDATA}
                                    keyExtractor={(item, index) => 'gridID_'+index}
                                    renderItem={ ({item}) => this.gridRenderOption(item)}
                                />
                            </View>
                            <ProgressBar />
                            <AppearenceSwitch />
                        </View>
                    </Animated.View>
                </Animated.View>
            </TouchableWithoutFeedback>
    )}
}
