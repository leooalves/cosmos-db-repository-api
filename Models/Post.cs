
using System;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;


namespace cosmos_db_repository_api.Models
{

    [PartitionKeyPath("/postId")]
    public class Post : Item
    {
        public DateTime DataCriacao { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string postId
        {
            get
            {
                return this.Id;
            }
        }        

        protected override string GetPartitionKeyValue() => postId;


    }
}