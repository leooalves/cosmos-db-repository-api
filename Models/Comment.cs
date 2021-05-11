
using System;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Attributes;


namespace cosmos_db_repository_api.Models
{

    [PartitionKeyPath("/postId")]
    public class Comment : Item
    {
        public DateTime DataCriacao { get; set; }        

        public string Conteudo { get; set; }

        public string commentId
        {
            get
            {
                return this.Id;
            }
        }

        public string postId { get; set; }

        protected override string GetPartitionKeyValue() => postId;


    }
}