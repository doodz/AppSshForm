namespace Doods.StdFramework.Http
{
    public class DoodsDownloadBytesProgress
    {
        public DoodsDownloadBytesProgress(string fileName, int bytesReceived, long totalBytes)
        {
            Filename = fileName;
            BytesReceived = bytesReceived;
            TotalBytes = totalBytes;
        }

        public long TotalBytes { get; private set; }

        public int BytesReceived { get; private set; }

        public float PercentComplete => (float)BytesReceived / TotalBytes;

        public string Filename { get; private set; }

        public bool IsFinished => BytesReceived == TotalBytes;
    }
}