using InstagramDataReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramDataReader.Instagram
{
    public static class InstagramHelper
    {
        public static void ReplaceSelfUser(this IInstagramConversations conversations, IInstagramProfile profile)
        {
            if (conversations == null)
                throw new ArgumentNullException(nameof(conversations));

            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            foreach (var conversation in conversations)
                conversation.RemoveParticipant(profile.UserName);
        }
    }
}
