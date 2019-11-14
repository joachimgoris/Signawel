using Signawel.Domain.Enums;
using System.Collections.Generic;

namespace Signawel.Dto.Determination
{
    public class DeterminationNodeCreatingRequestDto
    {

        public DeterminationNodeType Type { get; set; }

        public string Question { get; set; }
        
        public string QuestionDescription { get; set; }

        public string SchemaId { get; set; }

        public IList<DeterminationAnswerCreationRequestDto> Answers { get; set; }

    }
}