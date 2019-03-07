using System;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using InstagramDataReader.Interfaces;

namespace InstagramDataReader.Instagram
{
    [DebuggerDisplay("{UserName} - FullName: {FullName}, Email: {Email}")]
    public class InstagramProfile : InstagramDataElement<IInstagramProfile>, IInstagramProfile
    {
        public override string Name => "Profile";

        public virtual string File => "profile.json";

        [JsonProperty("date_joined")]
        public virtual DateTime Joined { get; set; }

        public virtual string Email { get; set; }

        public virtual string Genre { get; set; }

        [JsonProperty("private_account")]
        public virtual bool Private { get; set; }

        [JsonProperty("name")]
        public virtual string FullName { get; set; }

        [JsonProperty("phone_number")]
        public virtual string PhoneNumber { get; set; }

        [JsonProperty("profile_pic_url")]
        public virtual string ProfilePic { get; set; }

        [JsonProperty("username")]
        public virtual string UserName { get; set; }

        protected override IInstagramProfile CreateEntity(JToken jToken)
        {
            return jToken.ToObject<InstagramProfile>();
        }

        internal override void Load(JObject jObject)
        {
            var entity = CreateEntity(jObject.Root);

            Joined = entity.Joined;
            Email = entity.Email;
            Genre = entity.Genre;
            Private = entity.Private;
            FullName = entity.FullName;
            PhoneNumber = entity.PhoneNumber;
            ProfilePic = entity.ProfilePic;
            UserName = entity.UserName;
        }
    }
}
