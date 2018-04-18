using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using ResourceSyncSharp.Models;
using ResourceSyncSharp.Tags;

using JetBrains.Annotations;

namespace ResourceSyncSharp.Codecs
{
    [PublicAPI]
    public sealed class XmlCodec
    {
        private readonly ResourceSyncDocument _resourceSyncDocument;

        [PublicAPI]
        public XmlCodec(ResourceSyncDocument resourceSyncDocument)
        {
            _resourceSyncDocument = resourceSyncDocument;
        }

        [Pure]
        [PublicAPI]
        public static ResourceSyncDocument FromXml(XmlString xml)
        {
            Trace.WriteLine(xml);
            var lizer = new XmlSerializer(typeof(ResourceSyncDocument));
            using (var reader = new StringReader(xml))
            {
                return lizer.Deserialize(reader) as ResourceSyncDocument;
            }
        }

        [PublicAPI]
        public XmlString ToXml()
        {
            var lizer = new XmlSerializer(typeof(ResourceSyncDocument));
            Encoding utf8EncodingWithNoByteOrderMark = new UTF8Encoding(false);
            var settings = new XmlWriterSettings { Indent = true, NewLineOnAttributes = true, Encoding = utf8EncodingWithNoByteOrderMark };
            var stream = new MemoryStream();
            using (var writer = XmlWriter.Create(stream, settings))
            {
                return Serialize(writer, lizer, stream);
            }
        }

        private void AddXsltStylesheetLink(XmlWriter writer)
        {
            var baseUri = _resourceSyncDocument.BaseUri?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(baseUri))
            {
                baseUri = "/";
            }
            writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + baseUri + "sitemap.xsl\"");
        }

        private XmlString Serialize(XmlWriter writer, XmlSerializer lizer, MemoryStream stream)
        {
            AddXsltStylesheetLink(writer);
            lizer.Serialize(writer, _resourceSyncDocument, _resourceSyncDocument.Xmlns);
            var result = Encoding.UTF8.GetString(stream.ToArray());
            return result;
        }
    }
}