using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Framework
{
    /// <summary>
    /// Enumerator 
    /// </summary>
    public enum MessageStatus
    {
        Success = 0,
        Error = 1,
        Info = 2
    }

    /// <summary>
    /// Enumerator
    /// </summary>
    public enum FileType
    {
        ProfilePic,
        Image,
        Video,
        Document
    }

    /// <summary>
    /// Enumerator
    /// </summary>
    public enum UserType
    {
        GeneralUser = 1,
        Merchent = 2
    }

    /// <summary>
    /// Enumerator
    /// </summary>
    public enum Status
    {
        Deleted = 0,
        Active = 1,
    }

    /// <summary>
    /// Enumerator 
    /// </summary>
    public enum NotifyType
    {
        Success,
        Error,
        Info
    }

    /// <summary>
    /// Settings Enumerator 
    /// </summary>
    public enum AdminSettings
    {
        SocialMediaQualifierSet,
        MaxGroupsInCity,
    }

    /// <summary>
    /// Settings Enumerator 
    /// </summary>
    public enum SpecialAddOptions
    {
        NotPrinting=1,
        SelfDistributing=2,
    }

    public enum UserAdChoice
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 
        /// </summary>
        FollowCorporate = 1,
        /// <summary>
        /// 
        /// </summary>
        IndividualChoice = 2,
        /// <summary>
        /// 
        /// </summary>
        NotPrinting = 3,
        /// <summary>
        /// Doing Own Distribution (Creating Own Artwork)
        /// </summary>
        DoingOwnDistribution = 4,
    }
}

