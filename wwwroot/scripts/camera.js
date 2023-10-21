let dotnetHelper = null;
let streaming = false;
let videoElement = null;
let stream = null;
let isCameraRunning = false;

export function initialize(dotnetObjRef) {
    dotnetHelper = dotnetObjRef;
}

export function captureFrame() {
    const video = document.getElementById('webcam');
    const canvas = document.createElement('canvas');
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    canvas.getContext('2d').drawImage(video, 0, 0);
    return canvas.toDataURL('image/jpeg', 0.95);
}

export function isCameraActive() {
    return isCameraRunning;
}

export async function sendFrameToServer() {
    console.log("sendFrameToServer called");

    if (!isCameraRunning) {
        console.log("Camera is not active. Not sending frame.");
        return;
    }

    const frame = captureFrame();
    console.log("Frame captured");

    try {
        console.log("Trying to invoke C# method");
        await dotnetHelper.invokeMethodAsync('ProcessFrame', frame);
        console.log("C# method invoked successfully");
    } catch (error) {
        console.error("Error invoking C# method:", error);
    }
}

export function startCamera() {
    isCameraRunning = true;
    if (streaming) return;
    videoElement = document.getElementById('webcam');

    navigator.mediaDevices.getUserMedia({ video: true })
        .then((s) => {
            stream = s;
            videoElement.srcObject = s;
            videoElement.play();
            streaming = true;
        })
        .catch((err) => {
            console.error("An error occurred starting the camera:", err);
        });
}

export function stopCamera() {
    isCameraRunning = false;
    if (!streaming) return;

    let tracks = stream.getTracks();
    tracks.forEach((track) => {
        track.stop();
    });

    videoElement.srcObject = null;
    streaming = false;
}
