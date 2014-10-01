using System;
using System.Collections.Generic;
using System.Text;

namespace CmsMockObject.Objects
{
    // Summary:
    //      Represents the abstract base classes for Microsoft.ContentManagement.Publishing.CmsHttpContext
    //     and Microsoft.ContentManagement.Publishing.CmsApplicationContext.
    //
    // Remarks:
    //     Contains methods and properties that are common to Microsoft.ContentManagement.Publishing.CmsHttpContext
    //     and Microsoft.ContentManagement.Publishing.CmsApplicationContext . Users
    //     should not derive from this class.
    //     Microsoft.ContentManagement.Publishing.CmsContext is the base class for all
    //     classes that allow you to access the publishing API (PAPI). Instances of
    //     all other classes are retrieved directly or indirectly from these access
    //     points. As such, Microsoft.ContentManagement.Publishing.CmsContext provides
    //     access to the root containers, such as Microsoft.ContentManagement.Publishing.TemplateGallery,
    //     Microsoft.ContentManagement.Publishing.Channel, and Microsoft.ContentManagement.Publishing.ResourceGallery.
    //     Finally, CMSContext provides properties (such as Microsoft.ContentManagement.Publishing.CmsContext.UserCanModifySite
    //     ) that contain information about the currently authenticated users site-wide
    //     rights.
    public class CmsContext : IDisposable
    {
        // Summary:
        //      Gets a value indicating whether guest user access is enabled for MCMS.
        //
        // Remarks:
        //     Guess access is enabled using the Security tab in MCMS Server Configuration
        //     Application. Even if guest access is enabled, the guest account must be assigned
        //     permissions to Channels to enable anonymous access to the Postings in those
        //     channels. Guest permissions are configured using MCMS Site Manager.
        public bool IsDefaultGuestEnabled 
        {
            get { return false; } 
        }
        public bool IsLoggedInAsGuest 
        {
            get { return false; } 
        }
     //   public PublishingMode Mode 
        //{
       //     get { return null; }
       // }
        //public PlaceholderDefinitionTypeCollection PlaceholderDefinitionTypes { get; }
        public bool RollbackOnSessionEnd 
        {
            get { return false; }
            set { /*do nothing*/ }
        }
        public Channel RootChannel 
        {
            get { return new Channel(); } 
        }
//        public Searches Searches
//        { 
//            get {return null; } 
//        }

        public object Server 
        { 
            get { return null; } 
        }
        public DateTime ServerTime 
        {
            get { return DateTime.Now; } 
        }
        public object Session 
        {
            get { return null;  } 
        }
//        public SessionSettings SessionSettings 
//        {
//            get { return null; } 
//        }
        public string TemporaryUploadFolder
        {
            get { return ""; }
        }

        //public User User { get; }
        public bool UserCanApprove
        {
            get { return false; }
        }
        public bool UserCanAuthor
        {
            get { return false; }
        }
        public bool UserCanEditResources
        {
            get { return false; }
        }
        public bool UserCanEditTemplates
        {
            get { return false; }
        }
        public bool UserCanModifySite 
        {
            get { return false; }
        }
        public string AcceptBinaryFile(string filename)
        {
            return "";
        }
        public void CommitAll()
        {
        }
        public void Dispose()
        {
        }
        public void PropagateParameter(string parameterName, string parameterValue)
        {
        }
        public void RollbackAll()
        {
        }
        public bool UserHasRightToBrowse()
        {
            return false;
        }
        public bool UserHasRightToBrowse(string urlOrGuid)
        {
            return false;
        }
    }
}
