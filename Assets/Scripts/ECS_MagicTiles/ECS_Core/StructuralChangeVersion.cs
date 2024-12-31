using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS_Core
{
    public struct StructuralChangeVersion
    {
        public uint GlobalVersion;
        public uint FrameNumber;

        public void IncrementVersion()
        {
            GlobalVersion++;
        }

        public bool HasChanged(uint lastVersion) => GlobalVersion > lastVersion;
    }

    public struct QueryVersionInfo
    {
        public uint lastProcessedVersion;
        public uint lastUpdatedFrame;

        public bool IsValid(in StructuralChangeVersion worldVersion)
        {
            return lastProcessedVersion == worldVersion.GlobalVersion
                && lastUpdatedFrame == worldVersion.FrameNumber;
        }
    }
}
