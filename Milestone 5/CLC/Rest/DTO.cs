using CLC.Models.Game;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rest
{
    [DataContract]
    public class DTO
    {
        public DTO(int ErrorCode, string ErrorMsg, List<PublishedGame> Data)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMsg = ErrorMsg;
            this.Data = Data;
        }

        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public List<PublishedGame> Data { get; set; }
    }
}