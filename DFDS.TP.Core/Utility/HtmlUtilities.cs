using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DFDS.TP.Core.Utility;

public static class HtmlUtilities
{
    private static readonly Regex Tags = new("<[^>]*(>|$)", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    private static readonly Regex Whitelist = new(@"^</?(b(lockquote)?|code|d(d|t|l|el)|em|h(1|2|3)|i|kbd|li|ol|p(re)?|s(ub|up|trong|trike)?|ul)>$|^<(b|h)r\s?/?>$",
        RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

    private static readonly Regex WhitelistA
        = new(@"^<a\shref=""(\#\d+|(https?|ftp)://[-a-z0-9+&@#/%?=~_|!:,.;\(\)]+)""(\stitle=""[^""<>]+"")?\s?>$|^</a>$",
            RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

    private static readonly Regex WhitelistImg = new(@"^<img\ssrc=""https?://[-a-z0-9+&@#/%?=~_|!:,.;\(\)]+""
            (\swidth=""\d{1,3}"")?
            (\sheight=""\d{1,3}"")?
            (\salt=""[^""<>]*"")?
            (\stitle=""[^""<>]*"")?
            \s?/?>$", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

    private static readonly Regex NamedTags = new(@"</?(?<tagname>\w+)[^>]*(\s|$|>)", RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        
    public static string Safe(string html)
    {
        if (html.IsNullOrEmpty()) return html;

        html = Sanitize(html);
        html = BalanceTags(html);
        return html;
    }

    public static string Sanitize(string html)
    {
        if (html.IsNullOrEmpty()) return html;

        // match every HTML tag in the input
        var tags = Tags.Matches(html);
        for (var i = tags.Count - 1; i > -1; i--)
        {
            var tag = tags[i];
            var tagName = tag.Value.ToLowerInvariant();

            if (Whitelist.IsMatch(tagName) || WhitelistA.IsMatch(tagName) || WhitelistImg.IsMatch(tagName)) continue;

            html = html.Remove(tag.Index, tag.Length);
            Debug.WriteLine("tag sanitized: " + tagName);
        }
        return html;
    }

    public static string BalanceTags(string html)
    {
        if (html.IsNullOrEmpty()) return html;
            
        var tags = NamedTags.Matches(html.ToLowerInvariant());
            
        var tagCount = tags.Count;
        if (tagCount == 0) return html;

        const string ignoredTags = "<p><img><br><li><hr>";
        var tagPaired = new bool[tagCount];
        var tagRemove = new bool[tagCount];

        for (var countTag = 0; countTag < tagCount; countTag++)
        {
            var tagName = tags[countTag].Groups["tagname"].Value;

            if (tagPaired[countTag] || ignoredTags.Contains("<" + tagName + ">")) continue;

            var tag = tags[countTag].Value;
            var match = -1;

            if (tag.StartsWith("</"))
            {
                for (var previousTag = countTag - 1; previousTag >= 0; previousTag--)
                {
                    var prevTag = tags[previousTag].Value;
                    if (tagPaired[previousTag] || !prevTag.Equals("<" + tagName, StringComparison.InvariantCulture) ||
                        (!prevTag.StartsWith("<" + tagName + ">") && !prevTag.StartsWith("<" + tagName + " "))) continue;
              
                    match = previousTag;
                    break;
                }
            }
            else
            {
                // this is an opening tag
                // search forwards (next tags), look for closing tags
                for (var tagNumber = countTag + 1; tagNumber < tagCount; tagNumber++)
                {
                    if (tagPaired[tagNumber] || !tags[tagNumber].Value.Equals("</" + tagName + ">", StringComparison.InvariantCulture)) continue;
                
                    match = tagNumber;
                    break;
                }
            }
                
            tagPaired[countTag] = true;
            if (match == -1) tagRemove[countTag] = true;
            else tagPaired[match] = true; 
        }
            
        for (var tagNumber = tagCount - 1; tagNumber >= 0; tagNumber--)
        {
            if (!tagRemove[tagNumber]) continue;

            html = html.Remove(tags[tagNumber].Index, tags[tagNumber].Length);
            Debug.WriteLine("unbalanced tag removed: " + tags[tagNumber]);
        }
        return html;
    }

    public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);
}