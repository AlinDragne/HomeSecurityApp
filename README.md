# HomeSecurityApp

Data Analysis and Discussion

Introduction

The development of the HomeSecurityApp project took place through various stages, experiencing numerous successes, opportunities to learn, and unexpected obstacles. This chapter aims to clearly present the outcomes, including the results, challenges, and important insights, while also linking them back to the original research problem, objectives, and questions that guided this project.

Structure of the project:

1.	LiveView.razor
![Screenshot 2023-10-21 193139](https://github.com/AlinDragne/HomeSecurityApp/assets/80887719/4dbd9d18-cce3-4c4a-9465-3fd91abe16ed)
Fig. LiveView.razor
•	Purpose: Displays a live view, of the webcam.
•	Content: Uses the FaceDetectionService (for detecting faces in the live stream) and JavaScript (camera.js is imported) to use the webcam such as calling methods to manipulate the video stream.
2.	ManageRoles.razor
  ![Screenshot 2023-10-13 193132](https://github.com/AlinDragne/HomeSecurityApp/assets/80887719/3fae876d-964d-47ee-8b25-56758ade2355)
  Fig. ManageRoles – AdminAccount
  ![Screenshot 2023-10-13 193236](https://github.com/AlinDragne/HomeSecurityApp/assets/80887719/2eb9fb82-559f-4a53-8afc-af03ea0e6808)
  Fig. ManageRoles – ViewerAccount

•	Purpose: Manages user roles.
•	Content: Using .NET Core Identity, this page interacts with user and role management functionalities. Uses loops through a collection of “users”, emails and assigned roles are displayed in a table. This page is only accessible to users with the admin role.

3.	DetectedFaces.razor
 ![Screenshot 2023-10-13 193321](https://github.com/AlinDragne/HomeSecurityApp/assets/80887719/d5300036-9b4b-46c6-aceb-126e3c92f951)
Fig. DetectedFaces.razor

•	Purpose: Displays detected faces, grouping them by month.
•	Content: Images are grouped and displayed by month. ImageService is called to retrieve image data.
4.	ImageService.cs
•	Purpose: Manage and retrieve image file data.
•	Content: Contains a private read-only field that holds the path to the folder where image frames are stored. A method that retrieves file names from Frames folder, checks if the specified folder exists to prevent any error and ensures no null files are returned.
5.	IdentityDataInitialiser.cs
•	Purpose Initialise roles and an admin user in the application.
•	Content: Method to check if roles (Admin and Viewer) exist in the system, if not they are created, and another method to check if an admin user exist with a specified email and password and if not, it creates it.
6.	FaceDetectionService.cs
•	Purpose: Detect faces within given image data and perform to save them and highlight the faces with rectangles.
•	Content: Specifies the path to the Haar Cascade Classifier XML file, used for face detection logic. Utilises the Egmu.CV library (.NET wrapper for OpenCV) to detect faces within the image. Detected faces are hovered with rectangles drown around them. If any face is detected the image is being saved in a folder. 
7.	camera.js
•	Purpose: Manage the webcam and video streaming functionality.
•	Content: captureFrame() – Capture a single frame from the webcam(not being used in this state of the app), isCameraActive() - Check whether the camera is active (error prevention), sendFrameToServer() - If the camera is active, capture a frame and call ProcessFrame, to process the frame on the server-side.
8. Index.razor

Resu![Screenshot 2023-10-21 193109](https://github.com/AlinDragne/HomeSecurityApp/assets/80887719/75f4bcd6-e01e-4f14-8561-ca2767c70c89)


Results:
1.	Successes

•	Real-Time Face Detection: Accomplished through the implementation of Emgu CV in FaceDetectionService.cs, enabling the application to detect faces.

•	User Role Management: Using IdentityDataInitialiser.cs, the HomeSecurityApp sets up role-based access and adds initial data, making sure that access and user roles are managed in a structured way by using the Microsoft Identity library. This library is known for its strong abilities in handling user identities and access in ASP.NET Core applications and was key in making the user authentication and authorisation in the app smooth and straightforward. Using Microsoft Identity not only made the application’s security stronger by ensuring secure user authentication and role management but also offered a framework that is easy to manage and can be expanded in future versions of the application.


•	User Interaction and Data Display: Realised through Razor pages like DetectedFaces.razor and ManageRoles.razor, providing intuitive UIs for data interaction and management.

2.	Failures

•	Model Accuracy and Performance: The challenges in improving face detection were many. This included exploring different pre-trained models like tiny YOLOv2 and dealing with problems like faulty models and poor performance in various situations. Making sure the detection was accurate while also keeping it working efficiently was a particularly tricky challenge, especially for real-time face detection and processing. After considering these issues, the decision was made to stick with the Haar cascade pre-trained model. This choice was driven by its relative simplicity, ease of implementation, and proven efficacy in various scenarios, despite the obstacles encountered with other models.

•	Data Consistency: The application faced problems in ensuring consistent data seeding and managing potential conflicts or errors during user and role management processes. The adaptation from tutorials and implementations in .NET 5 to .NET 7 showed challenges in library compatibility and functionality, requiring careful modification and adaptation to ensure data integrity and consistency.

•	Usability and User Feedback: Creating a user-friendly experience, while also making sure users get clear feedback during use and when errors happen, was a complex task. This was made even harder by silent failures in the code, where problems didn’t show up as errors in the development environment or browser console. This required a careful and detailed process of debugging and improving the user experience.

•	Dependency and Library Management: Navigating through dependency management, particularly with NuGet packages, brought forth challenges in managing versions compatible with .NET 7.0, resolving issues related to missing .dll files, and ensuring clear and compatible package versions across the application.

•	Syntax and Semantic Issues: The code faced quiet problems with both syntax and meaning, from missed semicolons to extra curly braces. Even though these might seem like small issues, they require a deep dive into debugging. The silent issues, which didn't provide instant error feedback, showed how important careful code review and testing are to make sure the code works properly and without errors. Also, removing features and methods from classes and even .cs files, and forgetting to take them out from Program.cs or other files like .razor files, led to errors and required a complete rethinking of the solution.


•	Adaptation to Updated Technologies: Adapting methodologies and implementations from tutorials and resources based on older .NET versions to the utilised .NET 7 presented its own set of challenges. Ensuring that libraries and implementations were not only compatible but also optimized for .NET 7 required a thorough understanding and adaptation of the utilised technologies and libraries.

Every failure, while creating challenges during the development, was tackled with strategic problem-solving, research, and adaptation. These obstacles turned into valuable lessons and insights, each one uniquely helping the HomeSecurityApp to progress and improve.

Interpretation of Results

The mixing of data and information seen in the project shows an important truth: turning raw data, like image bytes and user inputs, into useful information, such as correctly identifying faces and managing user roles well, is complex. It needs careful creation of logic and user experience (UX).
Data -v- Information
•	Data: Consists of raw inputs, such as image bytes, user credentials, and roles.

•	Information: Transforms through processes like face detection, user authentication, and role authorisation, evolving into meaningful, actionable insights and functionalities within the app.
A manifestation of this can be observed in the FaceDetectionService class.
Raw Data: Image Bytes
public void DetectFacesAndSaveImage(byte[] imageBytes)
The method DetectFacesAndSaveImage receives raw data in the form of imageBytes. This byte array, which represents an image, lacks meaningful context on its own and is unprocessed, exemplifying data in its rawest form.
Process: Face Detection Algorithm
string faceModelPath = @"Data\haarcascade_frontalface_default.xml";
using var faceCascade = new Emgu.CV.CascadeClassifier(faceModelPath);
var faces = faceCascade.DetectMultiScale(image);
The raw image bytes go through a process of being processed via the face detection algorithm, utilising a Haar Cascade Classifier. The algorithm selects through the data, identifying and determining faces within the image, initiating the transformation of data into information.
Information: Detected Faces & Processed Images
foreach (var face in faces)
{
    CvInvoke.Rectangle(image, face, new Emgu.CV.Structure.MCvScalar(0, 0, 255), 3);
}
The processed image, now annotated with rectangles around detected faces, has transformed from mere data to information. It provides meaningful insight into the presence and location of faces within the image, which can be utilised for later processes or user interaction.
Utilisation and Storage of Information:
if (faces.Length > 0)
{
    string savePath = @"D:\VisualStudio\HomeSecurityApp\Data\Frames";
    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
    string fullPath = Path.Combine(savePath, fileName);
    CvInvoke.Imwrite(fullPath, image);
}
Upon detection of faces, the information is not only utilised to provide immediate output (attached image) but is also stored, encoding a time moment to it of when detection took place with a timestamp, preserving the information for future reference or analysis and avoiding naming errors that may occur when saving the frame.
Significance of Results
Combining all the successes and failures mentioned creates a deep pool of knowledge and learning. The importance is not just in the actual features of the HomeSecurityApp, but also in discovering possible improvements, scalability, and optimisations, giving a strong base for future developments and versions of the application.
Insights and Relating to Research Problem
The original research problem aimed to explore the development of a reliable, accessible, efficient home security application, leveraging facial recognition and structured user management. The results:
•	Address the Aim: By delivering a functional prototype that embodies real-time face detection and user role management.
•	Illuminate Objectives: By highlighting the technical and usability challenges in realising such a system, providing valuable insights into practical and theoretical considerations.
•	Answer Research Questions: By providing a tangible context through which theoretical concepts of facial recognition, user management, and UX design are navigated and understood.

Unexpected Outcomes

The journey of developing the HomeSecurityApp wasn’t just about expected challenges and hurdles but also tied to a lot of unexpected results and tricky obstacles that significantly influenced the app's development path.

One tricky challenge was dealing with instances where the code, even though it seemed to run fine, showed no errors either in the IDE (Visual Studio) or in the browser’s console log. The silent failure of the code, without any clear errors or warnings, required a detailed and intensive debugging process. Challenges varied from simple syntax errors, like forgotten “;” or extra “{}”, to more hidden complex issues, highlighting the subtle yet important role of syntactic accuracy in code execution and functionality.

Also, using external libraries and following online tutorials brought their own challenges. For example, adapting a tutorial on facial detection in .NET 5. While the tutorial offered a solid starting point, differences between library versions, especially moving to .NET 7, brought unexpected hurdles. The libraries, once working in .NET 5, didn’t directly transfer to .NET 7 due to updates, deprecated functions, or changed implementations in the new version. This meant exploring and understanding the updated libraries was necessary, requiring adaptations and changes to ensure they worked in .NET 7.

Working through these unexpected results and challenges, each obstacle was addressed with a strategic problem-solving approach. Whether it was deeply debugging silent failures, adjusting code to fix syntactic issues, or changing and improving implementations to accommodate updated libraries in .NET 7, each challenge turned into a chance for learning, adapting, and improving the HomeSecurityApp.

These detailed, unexpected results were welcomed and analysed, offering not only solutions to the immediate challenges but also uncovering deeper insights, learnings, and preventive strategies for future development work. The open discussion and exploration of these unexpected results demonstrate the complex nature of software development, where challenges are not just obstacles but chances for learning, adapting, and innovating.
Conclusion

The chapter, while highlighting the explicit functionalities, successes, and challenges encountered in the HomeSecurityApp project, also subtly unveils the implicit learnings, insights, and future potentials embedded within. The transparent discussion of every success, failure, and unexpected outcome, forms not only a reflective look back at the project but also a forward-looking perspective, paving the way towards future explorations, enhancements, and discoveries in the realm of home security applications.

