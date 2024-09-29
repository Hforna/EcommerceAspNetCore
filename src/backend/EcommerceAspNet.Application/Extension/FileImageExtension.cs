using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Application.Extension
{
    public static class FileImageExtension
    {
        public static (bool isImage, string typeImage) ValidateImage(Stream file)
        {
            var result = (false, string.Empty);

            if(file.Is<JointPhotographicExpertsGroup>())
                result = (true, FormatType(JointPhotographicExpertsGroup.TypeExtension));
            if (file.Is<PortableNetworkGraphic>())
                result = (true, FormatType(PortableNetworkGraphic.TypeExtension));

            file.Position = 0;

            return result;
        }

        public static string FormatType(string extension)
        {
            return extension.StartsWith(".") ? extension : $".{extension}";
        }
    }
}
