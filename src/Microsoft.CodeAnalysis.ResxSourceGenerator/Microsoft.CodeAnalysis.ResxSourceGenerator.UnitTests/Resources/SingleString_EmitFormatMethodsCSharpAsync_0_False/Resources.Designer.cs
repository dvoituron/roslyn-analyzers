﻿// <auto-generated/>

#nullable enable
using System.Reflection;


namespace TestProject
{
    /// <summary />
    internal static partial class Resources
    {
        private static global::System.Resources.ResourceManager? s_resourceManager;
        /// <summary />
        public static global::System.Resources.ResourceManager ResourceManager => s_resourceManager ?? (s_resourceManager = new global::System.Resources.ResourceManager(typeof(Resources)));
        /// <summary />
        public static global::System.Globalization.CultureInfo? Culture { get; set; }
        /// <summary />
        [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [return: global::System.Diagnostics.CodeAnalysis.NotNullIfNotNull("defaultValue")]
        internal static string? GetResourceString(string resourceKey, string? defaultValue = null) =>  ResourceManager.GetString(resourceKey, Culture) ?? defaultValue;
        /// <summary>value {0}</summary>
        public static string @Name => GetResourceString("Name")!;

    }
}
