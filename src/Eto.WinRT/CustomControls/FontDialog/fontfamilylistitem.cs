#if TODO_XAML
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Globalization;

namespace Eto.WinRT.CustomControls.FontDialog
{
    class FontFamilyListItem : TextBlock, IComparable
    {
        readonly string _displayName;

        public FontFamilyListItem(FontFamily fontFamily)
        {
            _displayName = GetDisplayName(fontFamily);

            this.FontFamily = fontFamily;
            this.Text = _displayName;
            this.ToolTip = _displayName;

            // In the case of symbol font, apply the default message font to the text so it can be read.
            if (IsSymbolFont(fontFamily))
            {
                var range = new TextRange(ContentStart, ContentEnd);
                range.ApplyPropertyValue(TextBlock.FontFamilyProperty, SystemFonts.MessageFontFamily);
            }
        }

        public override string ToString()
        {
            return _displayName;
        }

        int IComparable.CompareTo(object obj)
        {
            return string.Compare(_displayName, obj.ToString(), true, CultureInfo.CurrentCulture);
        }

        internal static bool IsSymbolFont(FontFamily fontFamily)
        {
            foreach (Typeface typeface in fontFamily.GetTypefaces())
            {
                GlyphTypeface face;
                if (typeface.TryGetGlyphTypeface(out face))
                {
                    return face.Symbol;
                }
            }
            return false;
        }

        internal static string GetDisplayName(FontFamily family)
        {
            return NameDictionaryHelper.GetDisplayName(family.FamilyNames);
        }
    }
}
#endif