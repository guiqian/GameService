using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameService;
using System;
using UnityEngine.UI;

public class DemoUIView : UIView<DemoUIView_ViewModel> {

    protected override void OnCreate(object[] _params) {
        base.OnCreate(_params);
        Bind<string, UnityEngine.UI.Text>("Child/MyText", ViewModel.mTextValue, OnValueChanged4MyText);
        Bind<float, UnityEngine.UI.Slider>("Child/MySlider", ViewModel.mSliderValue, OnValueChanged4MySlider);
        Bind<UIViewTestItemValue, UnityEngine.UI.Text>("Child/MyGroupText", ViewModel.mItemValue, OnValueChanged4MyGroupText);
        Bind<List<UIViewTestItemValue>, UnityEngine.UI.ScrollRect>("Child/MyScrollView", ViewModel.mListValue, onValueChanged4MyList);


        // =================set default value=================
        ViewModel.mTextValue.Value = "this default value";
        ViewModel.mSliderValue.Value = 0f;
        // ====================================================


        // ==============Test Item Value=======================
        UIViewTestItemValue itemValue = new UIViewTestItemValue();
        itemValue.yourName = "A";
        itemValue.yourAge = 10;
        ViewModel.mItemValue.Value = itemValue;
        // ====================================================


        // ==============Test List Value=======================
        List<UIViewTestItemValue> array = new List<UIViewTestItemValue>();
        UIViewTestItemValue one = new UIViewTestItemValue();
        one.yourName = "B"; one.yourAge = 11;
        array.Add(one);

        UIViewTestItemValue two = new UIViewTestItemValue();
        two.yourName = "C"; two.yourAge = 12;
        array.Add(two);
        ViewModel.mListValue.Value = array;
        // ====================================================
    }

    protected override void OnRestart(object[] _params) {
        base.OnRestart(_params);
    }

    protected override void OnClose() {
        base.OnClose();
    }

    protected override void OnPause() {
        base.OnPause();
    }

    protected override void OnResume() {
        base.OnResume();
    }

    private void OnValueChanged4MyText(string oldValue, string newValue, UnityEngine.UI.Text ui) {
        ui.text = newValue;
    }

    private void OnValueChanged4MySlider(float oldValue, float newValue, UnityEngine.UI.Slider ui) {
        ui.value = newValue;
    }

    private void OnValueChanged4MyGroupText(UIViewTestItemValue oldValue, UIViewTestItemValue newValue, Text component) {
        string str = string.Format("your new name : {0} and your new age : {1}", newValue.yourName, newValue.yourAge);
        component.text = str;
    }

    private void onValueChanged4MyList(List<UIViewTestItemValue> oldValue, List<UIViewTestItemValue> newValue, ScrollRect component) {
        Debug.Log("onValueChanged4MyList ValueChanged");
    }

    public void OnClickButtonSlider(GameObject view) {
        ViewModel.InvokeLocalMethod();
    }

    public void OnClickButtonText(GameObject view) {
        ViewModel.RequestHttp("your data");
    }

    public void OnClickButtonItemText(GameObject view) {
        ViewModel.InvokeItem();
    }

}
