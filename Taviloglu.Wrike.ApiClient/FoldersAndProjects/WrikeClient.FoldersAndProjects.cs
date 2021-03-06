﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taviloglu.Wrike.Core;
using Taviloglu.Wrike.Core.Json;

namespace Taviloglu.Wrike.ApiClient
{
    public partial class WrikeClient : IWrikeFoldersAndProjectsClient
    {
        public IWrikeFoldersAndProjectsClient FoldersAndProjects
        {
            get
            {
                return (IWrikeFoldersAndProjectsClient)this;
            }
        }
        async Task<List<WrikeFolder>> IWrikeFoldersAndProjectsClient.GetFoldersAsync(List<string> folderIds, List<string> optionalFields)
        {

            if (folderIds == null || folderIds.Count < 1)
            {
                throw new ArgumentNullException("folderIds can not be null or empty");
            }
            if (folderIds.Count > 100)
            {
                throw new ArgumentException("folderIds max count is 100");
            }

            var requestUri = "folders/" + string.Join(",", folderIds);

            if (optionalFields != null && optionalFields.Count > 0)
            {
                requestUri += "?fields=" + JsonConvert.SerializeObject(optionalFields);
            }

            var response = await SendRequest<WrikeFolder>(requestUri, HttpMethods.Get).ConfigureAwait(false);
            return GetReponseDataList(response);
        }

        async Task<List<WrikeFolderTree>> IWrikeFoldersAndProjectsClient.GetFolderTreeAsync(string accountId, string folderId, string permalink, bool? addDescendants, WrikeMetadata metadata, WrikeCustomFieldData customField, WrikeDateFilterRange updatedDate, bool? isProject, bool? isDeleted, List<string> fields)
        {
            if (!string.IsNullOrWhiteSpace(accountId) && !string.IsNullOrWhiteSpace(folderId))
            {
                throw new ArgumentException("only folderId or accountId can be used, not both!");
            }

            var requestUri = "folders";

            if (!string.IsNullOrWhiteSpace(accountId))
            {
                requestUri = $"accounts/{accountId}/folders";
            }
            else if (!string.IsNullOrWhiteSpace(folderId))
            {
                requestUri = $"folders/{folderId}/folders";
            }

            var uriBuilder = new WrikeGetUriBuilder(requestUri)
            .AddParameter("permalink", permalink)
            .AddParameter("descendants", addDescendants)
            .AddParameter("metadata", metadata)
            .AddParameter("customField", customField)
            .AddParameter("updatedDate", updatedDate, new CustomDateTimeConverter("yyyy-MM-dd'T'HH:mm:ss'Z'"))
            .AddParameter("project", isProject)
            .AddParameter("fields", fields);
            if (string.IsNullOrWhiteSpace(folderId))
            {
                uriBuilder.AddParameter("deleted", isDeleted);
            }

            var response = await SendRequest<WrikeFolderTree>(uriBuilder.GetUri(), HttpMethods.Get).ConfigureAwait(false);
            return GetReponseDataList(response);
        }

        async Task<WrikeFolder> IWrikeFoldersAndProjectsClient.CreateAsync(string folderId, WrikeFolder newFolder)
        {
            if (string.IsNullOrWhiteSpace(folderId))
            {
                throw new ArgumentNullException(nameof(folderId), "folderId can not be null or empty");
            }

            if (string.IsNullOrWhiteSpace(newFolder.Title))
            {
                throw new ArgumentNullException(nameof(newFolder.Title), "newFolder.Title can not be null or empty");
            }

            var requestUri = $"folders/{folderId}/folders";

            var postDataBuilder = new WrikeFormUrlEncodedContentBuilder()
                .AddParameter("title", newFolder.Title)
                .AddParameter("description", newFolder.Description)
                .AddParameter("shareds", newFolder.SharedIds)
                .AddParameter("metadata", newFolder.Metadata)
                .AddParameter("customFields", newFolder.CustomFields)
                .AddParameter("customColumns", newFolder.CustomColumnIds)
            .AddParameter("project", newFolder.Project);

            var postContent = postDataBuilder.GetContent();
            var response = await SendRequest<WrikeFolder>(requestUri, HttpMethods.Post, postContent).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }

        async Task<WrikeFolder> IWrikeFoldersAndProjectsClient.DeleteAsync(string folderId)
        {
            if (string.IsNullOrWhiteSpace(folderId))
            {
                throw new ArgumentNullException("folderId can not be null or empty");
            }

            var response = await SendRequest<WrikeFolder>($"folders/{folderId} ", HttpMethods.Delete).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }

        async Task<WrikeFolder> IWrikeFoldersAndProjectsClient.UpdateAsync(string folderId, string title, string description, List<string> addParents, List<string> removeParents, List<string> addShareds, List<string> removeShareds, List<WrikeMetadata> metadata, bool? restore, List<WrikeCustomFieldData> customFields, List<string> customColumns, WrikeProject project) {


            if (string.IsNullOrWhiteSpace(folderId))
            {
                throw new ArgumentNullException(nameof(folderId), "folderId can not be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title), "title can not be null or empty.");
            }

            var contentBuilder = new WrikeFormUrlEncodedContentBuilder()
                .AddParameter("title", title)
                .AddParameter("description", description)
                .AddParameter("addParents", addParents)
                .AddParameter("removeParents", removeParents)
                .AddParameter("addShareds", addShareds)
                .AddParameter("removeShareds", removeShareds)
                .AddParameter("metadata", metadata)
                .AddParameter("restore", restore)
                .AddParameter("customFields", customFields)
                .AddParameter("customColumns", customColumns)
                .AddParameter("project", project);

            var response = await SendRequest<WrikeFolder>($"folders/{folderId}", HttpMethods.Put, contentBuilder.GetContent()).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }
    }
}
