using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public enum UIElementType {
        None = 0,
        Button = 1,
        Slider = 2,
        Image,
        Text,
        ScrollRect,
        RawImage,
        Toggle,
        ToggleGroup,
        InputField,
        Scrollbar,
        RectMask2D,
        Mask,
        LayoutGroup,
        LayoutElement,
        GridLayoutGroup,
        Graphic,
        CanvasScaler,
        VerticalLayoutGroup,
        HorizontalLayoutGroup,
        HorizontalOrVerticalLayoutGroup,
        MaskableGraphic,
        Outline,
        Shadow,
    }

    public sealed class UIElementMapping {

        private static Dictionary<UIElementType, System.Type> mapping = new Dictionary<UIElementType, System.Type>();

        static UIElementMapping() {
            mapping.Clear();
            mapping.Add(UIElementType.Button, typeof(UnityEngine.UI.Button));
            mapping.Add(UIElementType.Slider, typeof(UnityEngine.UI.Slider));
            mapping.Add(UIElementType.Image, typeof(UnityEngine.UI.Image));
            mapping.Add(UIElementType.Text, typeof(UnityEngine.UI.Text));
            mapping.Add(UIElementType.RawImage, typeof(UnityEngine.UI.RawImage));
            mapping.Add(UIElementType.Toggle, typeof(UnityEngine.UI.Toggle));
            mapping.Add(UIElementType.ToggleGroup, typeof(UnityEngine.UI.ToggleGroup));
            mapping.Add(UIElementType.InputField, typeof(UnityEngine.UI.InputField));
            mapping.Add(UIElementType.Scrollbar, typeof(UnityEngine.UI.Scrollbar));
            mapping.Add(UIElementType.ScrollRect, typeof(UnityEngine.UI.ScrollRect));
            mapping.Add(UIElementType.RectMask2D, typeof(UnityEngine.UI.RectMask2D));
            mapping.Add(UIElementType.Mask, typeof(UnityEngine.UI.Mask));
            mapping.Add(UIElementType.LayoutGroup, typeof(UnityEngine.UI.LayoutGroup));
            mapping.Add(UIElementType.LayoutElement, typeof(UnityEngine.UI.LayoutElement));
            mapping.Add(UIElementType.GridLayoutGroup, typeof(UnityEngine.UI.GridLayoutGroup));
            mapping.Add(UIElementType.Graphic, typeof(UnityEngine.UI.Graphic));
            mapping.Add(UIElementType.CanvasScaler, typeof(UnityEngine.UI.CanvasScaler));
            mapping.Add(UIElementType.VerticalLayoutGroup, typeof(UnityEngine.UI.VerticalLayoutGroup));
            mapping.Add(UIElementType.HorizontalLayoutGroup, typeof(UnityEngine.UI.HorizontalLayoutGroup));
            mapping.Add(UIElementType.HorizontalOrVerticalLayoutGroup, typeof(UnityEngine.UI.HorizontalOrVerticalLayoutGroup));
            mapping.Add(UIElementType.MaskableGraphic, typeof(UnityEngine.UI.MaskableGraphic));
            mapping.Add(UIElementType.Outline, typeof(UnityEngine.UI.Outline));
            mapping.Add(UIElementType.Shadow, typeof(UnityEngine.UI.Shadow));
        }

        public static System.Type FindUGUIType(UIElementType uiType) {
            System.Type type = null;
            if (mapping.TryGetValue(uiType, out type)) {
            }
            return type;
        }

    }
}