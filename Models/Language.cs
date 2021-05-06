using System;
using Microsoft.Azure.CosmosRepository;
using cosmos_db_repository_api.Enums;

namespace cosmos_db_repository_api.Models
{
    public class Language : Item
    {
        public string Name { get; set; }

        public string[] Aliases { get; set; }

        public string Description { get; set; }

        public EProgrammingStyle PrimaryStyle { get; set; }

        public DateTime InitialReleaseDate { get; set; }
    }   
}