using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Polygon : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<Polygon, UxmlTraits> { }

    const string styleSheet = "PolygonStyles";
    const string externalStyle = "polygon_container";
    const string polygonStyle = "polygon_border-image";
    const string iconStyle = "polygon_icon";
    const string labelStyle = "polygon_label";

    readonly VisualElement polygonBorder = new();
    readonly Label label = new();
    readonly VisualElement icon = new();

    private string text;
    public string Text
    {
        get => text;
        set => label.text = text = value;
    }

    public string PolyImagePath { get; set; }

    public string IconPath { get; set; }

    public float IconScale { get; set; }


    public Polygon() : base()
    {
        styleSheets.Add(Resources.Load<StyleSheet>(styleSheet));

        AddToClassList(externalStyle);
        polygonBorder.AddToClassList(polygonStyle);
        label.AddToClassList(labelStyle);
        icon.AddToClassList(iconStyle);
    }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription uText = new() { name = "text", defaultValue = "C9" };
        UxmlStringAttributeDescription uPolyImagePath = new() { name = "poly-image-path", defaultValue = "" };
        UxmlStringAttributeDescription uIconPath = new() { name = "icon-path", defaultValue = "" };
        UxmlFloatAttributeDescription uIconScale = new() { name = "icon-scale", defaultValue = 1f };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var p = ve as Polygon;
            
            p.Text = uText.GetValueFromBag(bag, cc);
            p.PolyImagePath = uPolyImagePath.GetValueFromBag(bag, cc);
            p.IconPath = uIconPath.GetValueFromBag(bag, cc);
            p.IconScale = uIconScale.GetValueFromBag(bag, cc);

            p.Clear();
            p.Add(p.polygonBorder);
            p.Add(p.label);
            p.polygonBorder.Add(p.icon);

            Texture2D poly = Resources.Load<Texture2D>(p.PolyImagePath);           
            if (poly)
                p.polygonBorder.style.backgroundImage = poly;

            Texture2D icon = Resources.Load<Texture2D>(p.IconPath);
            if (icon)
            {
                p.icon.style.opacity = 1f;
                p.icon.style.scale = new Scale(new Vector2(p.IconScale, p.IconScale));
                p.icon.style.backgroundImage = icon;
            }               
        }
    }
}
