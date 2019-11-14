using Newtonsoft.Json;
using Signawel.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Signawel.Dto.Determination
{
    public class DeterminationNodeResponseDto
    {

        public DeterminationNodeType Type { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string Question { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string QuestionDescription { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string SchemaId { get; set; }

        public IList<DeterminationAnswerResponseDto> Answers { get; set; }

        public bool ShouldSerializeAnswers()
        {
            return Answers != null && Answers.Any();
        }

    }
}