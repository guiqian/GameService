using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameService;
using System;

public class UIViewTestItemValue {
    public string yourName = null;
    public int yourAge = 0;
}

public class DemoUIView_ViewModel : IViewModelListener {

    public BindablePropertyUIElement<string, UnityEngine.UI.Text> mTextValue = null;
    public BindablePropertyUIElement<float, UnityEngine.UI.Slider> mSliderValue = null;
    public BindablePropertyUIElement<UIViewTestItemValue, UnityEngine.UI.Text> mItemValue = null;
    public BindablePropertyUIElement<List<UIViewTestItemValue>, UnityEngine.UI.ScrollRect> mListValue = null;

    public DemoUIView_ViewModel() {
        mTextValue = new BindablePropertyUIElement<string, UnityEngine.UI.Text>();
        mSliderValue = new BindablePropertyUIElement<float, UnityEngine.UI.Slider>();
        mItemValue = new BindablePropertyUIElement<UIViewTestItemValue, UnityEngine.UI.Text>();
        mListValue = new BindablePropertyUIElement<List<UIViewTestItemValue>, UnityEngine.UI.ScrollRect>();
    }

    public void InvokeLocalMethod() {
        mSliderValue.Value += 0.1f;
    }

    public void InvokeItem() {
        mItemValue.Value.yourAge++;
        mItemValue.NotifyOnValueChanged();
    }

    public void RequestHttp(string yourData) {
        IEnumerator task = null;
        task = OnTaskHttp(() => {
            mTextValue.Value = "this data from network";
            GameServiceProxy.Service_Coroutine.StopCoroutine(task);
            task = null;
        });
        GameServiceProxy.Service_Coroutine.StartCoroutine(task);
    }

    private IEnumerator OnTaskHttp(System.Action callback = null) {
        yield return new WaitForSeconds(5f);
        if (callback != null) {
            callback();
        }
    }

    void System.IDisposable.Dispose() {
        mTextValue.Dispose();
        mTextValue = null;
        mSliderValue.Dispose();
        mSliderValue = null;
    }

}
