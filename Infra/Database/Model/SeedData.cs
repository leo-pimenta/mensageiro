using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain;

namespace Infra.Database.Model
{
    public class SeedData
    {
        public ReadOnlyCollection<User> Users { get; }

        public ReadOnlyCollection<Contact> Contacts { get; }

        public SeedData()
        {
            this.Users = new List<User>()
            {
                new User(new Guid("d9f0c3e1-02f6-4ce5-bf74-b7c0f14cf2d2"), "joao.teste@teste.com", "João"),
                new User(new Guid("8a4b6a86-a053-46ac-9ba6-04eacaf5bf7d"), "leo.teste@teste.com", "Leo"),
                new User(new Guid("3fc9e8d0-9a65-459f-ade4-57fe754f7596"), "mariana.teste@teste.com", "Mariana"),
                new User(new Guid("1e35ccb4-7d5a-4747-9cb0-62a875f44fd5"), "matheus.teste@teste.com", "Matheus"),
                new User(new Guid("4faaf336-27d2-4680-a2ae-78ec6c0b4162"), "claudia.teste@teste.com", "Claudia"),
                new User(new Guid("7b2601b8-0af4-43d3-9dda-f1db0cd7dd51"), "luisfelipe.teste@teste.com", "Luís Felipe"),
            }.AsReadOnly();

            var group01 = new ChatGroup(new Guid("2b903f40-1f53-4c5b-beb3-b3cf5ac21ff3"));
            var group02 = new ChatGroup(new Guid("998f3c13-78f1-42cb-977d-dfa121ae3609"));
            var group03 = new ChatGroup(new Guid("04b8a481-221f-4bef-a5af-306204ced472"));
            var group04 = new ChatGroup(new Guid("649a3553-5da3-4ce8-ad49-c51714bef758"));
            var group05 = new ChatGroup(new Guid("ffb2fe92-612f-41c2-b77c-b9027e430be1"));
            var group12 = new ChatGroup(new Guid("0d9bcff0-57ea-45ed-a566-3c829942e34b"));
            var group13 = new ChatGroup(new Guid("167096b8-e066-46f0-a90e-446b540d331d"));
            var group14 = new ChatGroup(new Guid("169e4880-3d26-4e5b-a14c-c1e214d17370"));
            var group15 = new ChatGroup(new Guid("a125b75d-abce-42f6-b876-f18978312f4d"));
            var group23 = new ChatGroup(new Guid("a498dcb0-3f6d-4f39-a354-9af1e18f3d9e"));
            var group24 = new ChatGroup(new Guid("95bba9f6-c661-425f-9902-dcb08e00d949"));
            var group25 = new ChatGroup(new Guid("9ed9e0da-4c7e-4071-82ea-7f3617abde35"));
            var group34 = new ChatGroup(new Guid("7dc46b57-9f16-4857-9951-60b65e8d68ab"));
            var group35 = new ChatGroup(new Guid("a5836c08-6aa5-4d85-8c18-af8019cdec9f"));
            var group45 = new ChatGroup(new Guid("8f525afe-0b26-4e29-8edb-ebaa62721a4e"));

            this.Contacts = new List<Contact>()
            {
                new Contact(new Guid("1b17a4dd-1979-4d0c-97fe-23ab21427c5e"), this.Users[0], this.Users[1], group01),
                new Contact(new Guid("e2751d60-5186-4d62-9031-66db81bdc1a7"), this.Users[0], this.Users[2], group02),
                new Contact(new Guid("3947be5d-b018-45fa-9850-957e2ec079fa"), this.Users[0], this.Users[3], group03),
                new Contact(new Guid("6705141b-072b-44b0-9712-fb1fde986346"), this.Users[0], this.Users[4], group04),
                new Contact(new Guid("b313be69-5e66-4f93-bc56-b0661bc7dd15"), this.Users[0], this.Users[5], group05),

                new Contact(new Guid("3a7eadd1-c00c-470b-a18d-dfce31c28b01"), this.Users[1], this.Users[0], group01),
                new Contact(new Guid("c9966289-f5a5-4caa-80cc-0be3b812094a"), this.Users[1], this.Users[2], group12),
                new Contact(new Guid("44b88124-4da1-4bf7-91f5-f04346b452ab"), this.Users[1], this.Users[3], group13),
                new Contact(new Guid("2bf78af2-7e55-44fb-9017-ecb77de24cb1"), this.Users[1], this.Users[4], group14),
                new Contact(new Guid("6fd926de-c320-4834-b321-8ffef4807650"), this.Users[1], this.Users[5], group15),

                new Contact(new Guid("e1f830b8-8974-44d4-b54b-8aa49ee03c5d"), this.Users[2], this.Users[0], group02),
                new Contact(new Guid("71253f3e-484b-42ac-99f1-8348299b55ad"), this.Users[2], this.Users[1], group12),
                new Contact(new Guid("c7704f7c-f51d-4160-9459-f75131532903"), this.Users[2], this.Users[3], group23),
                new Contact(new Guid("b398b8c8-c896-4eb5-a05a-a7037c2a6d8b"), this.Users[2], this.Users[4], group24),
                new Contact(new Guid("14845c01-8314-4569-872c-e8abb474a067"), this.Users[2], this.Users[5], group25),
                
                new Contact(new Guid("4d017a0e-10fc-4445-9211-63f59fe58dfe"), this.Users[3], this.Users[0], group03),
                new Contact(new Guid("bd9e0c52-5ca1-4b6b-930d-b5b47992727b"), this.Users[3], this.Users[1], group13),
                new Contact(new Guid("d674bd37-3f1c-4055-8e4f-265314ea729d"), this.Users[3], this.Users[2], group23),
                new Contact(new Guid("a6689f02-d9fa-4d29-8222-bc1c8278f740"), this.Users[3], this.Users[4], group34),
                new Contact(new Guid("5b314bb3-3a91-4185-afef-08b161c2aef0"), this.Users[3], this.Users[5], group35),

                new Contact(new Guid("ea263365-2265-49f4-acb6-1f4627ca5abe"), this.Users[4], this.Users[0], group04),
                new Contact(new Guid("bdfb7c71-94f6-4eaf-b44a-2dfc9da95cb4"), this.Users[4], this.Users[1], group14),
                new Contact(new Guid("2e463f58-a3b9-4c81-84da-8450f8a91467"), this.Users[4], this.Users[2], group24),
                new Contact(new Guid("ae2f939b-8995-4e8c-8d84-ed59fd667d64"), this.Users[4], this.Users[3], group34),
                new Contact(new Guid("ec87c7e3-6aab-4ac7-9c84-f408d7a38d6e"), this.Users[4], this.Users[5], group45),

                new Contact(new Guid("6286f15f-9d90-428f-9670-b6dd13110a4b"), this.Users[5], this.Users[0], group05),
                new Contact(new Guid("906d3ae8-c93b-4710-a33c-9ac946a47931"), this.Users[5], this.Users[1], group15),
                new Contact(new Guid("5a12bb8f-6740-4c0d-86b8-4e8b1395234f"), this.Users[5], this.Users[2], group25),
                new Contact(new Guid("c49b428f-1f4b-4870-b4e7-441052630305"), this.Users[5], this.Users[3], group35),
                new Contact(new Guid("77d5673a-2b25-416a-854f-3c9fb537b914"), this.Users[5], this.Users[4], group45)
            }.AsReadOnly();
        }
    }
}