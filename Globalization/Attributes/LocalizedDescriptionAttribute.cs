using System.ComponentModel;
using System.Resources;

namespace Globalization.Attributes;

    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string resourceKey;

    private static ResourceManager _resourceManagerProp { get; set; }

    public LocalizedDescriptionAttribute(string resourceKey)
    {
        this.resourceKey = resourceKey;
    }

    public override string Description
    {
        get => this.resourceKey != null ? _resourceManagerProp.GetString(this.resourceKey) : null;
    }

    public static void Setup(ResourceManager resourceManager)
    {
        _resourceManagerProp = resourceManager;
    }
}

