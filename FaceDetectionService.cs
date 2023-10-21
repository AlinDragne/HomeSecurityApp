using Emgu.CV.CvEnum;
using Emgu.CV;

namespace HomeSecurityApp
{
    public class FaceDetectionService
    {
        public void DetectFacesAndSaveImage(byte[] imageBytes)
        {
            string faceModelPath = @"Data\haarcascade_frontalface_default.xml";
            using var faceCascade = new Emgu.CV.CascadeClassifier(faceModelPath);

            Mat image = new Mat();
            CvInvoke.Imdecode(imageBytes, ImreadModes.Color, image);
            var faces = faceCascade.DetectMultiScale(image);

            foreach (var face in faces)
            {
                CvInvoke.Rectangle(image, face, new Emgu.CV.Structure.MCvScalar(0, 0, 255), 3);
            }

            if (faces.Length > 0)
            {
                string savePath = @"D:\VisualStudio\HomeSecurityApp\Data\Frames";
                string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
                string fullPath = Path.Combine(savePath, fileName);
                CvInvoke.Imwrite(fullPath, image);
            }
        }
    }

}
