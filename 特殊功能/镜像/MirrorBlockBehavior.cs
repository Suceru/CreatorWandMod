using Engine;

namespace CreatorModAPI
{
    public static class MirrorBlockBehavior
    {
        // static Point3 PreStart, PreEnd;
        public static bool IsMirror = false;
        public static void ChangeIsMirror()
        {
            if (IsMirror)
            {
                IsMirror = false;
            }
            else
            {
                IsMirror = true;
            }
        }
        public static void Preview(CreatorAPI creatorAPI, ref Point3 Start, ref Point3 End)
        {
            /*  CPPreview.PreStart = Start;
              CPPreview.PreEnd = End;
          */
        }
    }
}
