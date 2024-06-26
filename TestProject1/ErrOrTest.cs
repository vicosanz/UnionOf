using UnionOf;

namespace TestProject1
{
    public class ErrOrTest
    {
        Request request = new(Guid.NewGuid(), "Infoware");
        Request request2 = new(Guid.NewGuid(), "");
        Request request3 = new(Guid.NewGuid(), null!);


        [Fact]
        public void TestConstructor()
        {
            var result = new ErrOr<Request>(request);
            Assert.Equal(request, result);
        }
        [Fact]
        public void TestOf()
        {
            var result = ErrOr.Of(request);
            Assert.Equal(request, result);
        }
        [Fact]
        public void TestMapSync()
        {
            var result = ErrOr.Of(request)
                .Map(ValidateNonEmpty);
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            var result2 = ErrOr.Of(request2)
                .Map(ValidateNonEmpty);
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            Assert.NotEqual(request2, result2);
            Assert.True(result2.IsFail());

            var result3 = ErrOr.Of(request)
                .Map(ToUpper);

            Assert.NotEqual(request, result3);
            Assert.True(result3.Is(out Request req) && req.Name == "INFOWARE");
        }
        [Fact]
        public void TestMapSync2()
        {
            var result = ErrOr.Of(request)
                .Map(x =>
                {
                    if (string.IsNullOrWhiteSpace(x.Name))
                    {
                        return new Exception("Name is empty");
                    }
                    return x;
                });
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            var result2 = ErrOr.Of(request2)
                .Map(x =>
                {
                    if (string.IsNullOrWhiteSpace(x.Name))
                    {
                        return new Exception("Name is empty");
                    }
                    return x;
                });
            Assert.NotEqual(request2, result2);
            Assert.True(result2.IsFail());
        }

        [Fact]
        public async Task TestMapAsync()
        {
            var result = await ErrOr.Of(request)
                .MapAsync(ValidateNonEmptyAsync);
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            var result2 = await ErrOr.Of(request2)
                .MapAsync(ValidateNonEmptyAsync);
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            Assert.NotEqual(request2, result2);
            Assert.True(result2.IsFail());
        }

        [Fact]
        public async Task TestDoubleMapAsync()
        {
            var result = await ErrOr.Of(request)
                .MapAsync(ValidateNonEmptyAsync)
                .MapAsync(ValidateNonEmptyAsync)
                .MapAsync(ValidateNonEmpty);
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            var result2 = await ErrOr.Of(request2)
                .MapAsync(ValidateNonEmptyAsync)
                .MapAsync(ValidateNonEmptyAsync)
                .MapAsync(ValidateNonEmpty);
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            Assert.NotEqual(request2, result2);
            Assert.True(result2.IsFail());
        }
        [Fact]
        public async Task TestMapAsync2Async()
        {
            var result = await ErrOr.Of(request)
                .MapAsync(async x =>
                {
                    await Task.Delay(1);
                    if (string.IsNullOrWhiteSpace(x.Name))
                    {
                        return new Exception("Name is empty");
                    }
                    return x;
                });
            Assert.Equal(request, result);
            Assert.False(result.IsFail());

            var result2 = await ErrOr.Of(request2)
                .MapAsync(async x =>
                {
                    await Task.Delay(1);
                    if (string.IsNullOrWhiteSpace(x.Name))
                    {
                        return new Exception("Name is empty");
                    }
                    return x;
                });
            Assert.NotEqual(request2, result2);
            Assert.True(result2.IsFail());
        }
        [Fact]
        public void TestBindSync()
        {
            var result = ErrOr.Of(request)
                .Map(ValidateNonEmpty)
                .Bind(ToStringResponse);
            Assert.True(result.IsValid());
            Assert.Equal("Infoware", result);

            var result2 = ErrOr.Of(request2)
                .Map(ValidateNonEmpty)
                .Bind(ToStringResponse);
            Assert.False(result2.IsValid());
            Assert.IsType<Exception>(result2.Value);
        }
        [Fact]
        public async Task TestBindAsync()
        {
            var result = await ErrOr.Of(request)
                .Map(ValidateNonEmpty)
                .BindAsync(ToStringResponseAsync)
                .BindAsync<string, string>(x => x + "1")
                .BindAsync<string, string>(x => x + "2")
                .BindAsync<string, string>(async x =>
                {
                    await Task.Delay(0);
                    return x + "3";
                })
                .BindAsync<string, string>(async x =>
                 {
                     await Task.Delay(0);
                     return x + "4";
                 });
            Assert.True(result.IsValid());
            Assert.Equal("Infoware1234", result);

            var result2 = ErrOr.Of(request2)
                .Map(ValidateNonEmpty)
                .Bind(ToStringResponse);
            Assert.False(result2.IsValid());
            Assert.IsType<Exception>(result2.Value);
        }
        [Fact]
        public async Task TestMatchSyncAsync()
        {
            var result = ErrOr.Of(request)
                .Map(ValidateNonEmpty)
                .Match(
                    valid => valid.Name,
                    error => error.Message
                );
            Assert.Equal("Infoware", result);

            var result2 = ErrOr.Of(request2)
                .Map(ValidateNonEmpty)
                .Match(
                    valid => valid.Name,
                    error => error.Message
                );
            Assert.NotEqual("Infoware", result2);

            var result3 = await ErrOr.Of(request)
                .Map(ValidateNonEmpty)
                .MatchAsync(
                    async valid =>
                    {
                        await Task.Delay(1);
                        return valid.Name;
                    },
                    error => error.Message
                );
            Assert.Equal("Infoware", result3);

            var result4 = await ErrOr.Of(request2)
                .Map(ValidateNonEmpty)
                .MatchAsync(
                    valid => valid.Name,
                    async error =>
                    {
                        await Task.Delay(1);
                        return error.Message;
                    }
                );
            Assert.NotEqual("Infoware", result4);

            var result4a = await ErrOr.Of(request2)
                .Map(ValidateNonEmpty)
                .MatchAllAsync(
                    async valid =>
                    {
                        await Task.Delay(1);
                        return valid.Name;
                    },
                    async error =>
                    {
                        await Task.Delay(1);
                        return error.Message;
                    }
                );
            Assert.NotEqual("Infoware", result4a);

            var result5 = await ErrOr.Of(request)
                .MapAsync(ValidateNonEmptyAsync)
                .MatchAsync(
                    async valid =>
                    {
                        await Task.Delay(1);
                        return valid.Name;
                    },
                    error => error.Message
                );
            Assert.Equal("Infoware", result5);

            var result6 = await ErrOr.Of(request2)
                .MapAsync(ValidateNonEmptyAsync)
                .MatchAsync(
                    valid => valid.Name,
                    async error =>
                    {
                        await Task.Delay(1);
                        return error.Message;
                    }
                );
            Assert.NotEqual("Infoware", result6);

            var result6a = await ErrOr.Of(request2)
                .MapAsync(ValidateNonEmptyAsync)
                .MatchAllAsync(
                    async valid =>
                    {
                        await Task.Delay(1);
                        return valid.Name;
                    },
                    async error =>
                    {
                        await Task.Delay(1);
                        return error.Message;
                    }
                );
            Assert.NotEqual("Infoware", result6a);

            var result7 = await ErrOr.Of(request2)
                .MapAsync(ValidateNonEmptyAsync)
                .MatchAsync(
                    valid => valid.Name,
                    error => error.Message
                );
            Assert.NotEqual("Infoware", result7);
        }

        [Fact]
        public void TryBind()
        {
            var result = ErrOr.Of(request3)
                .TryBind<Request, string>(x => x.Name, () => new Exception("null"))
                .TryBind<string, string>(x => x, () => new Exception("null"));

            Assert.IsType<Exception>(result.Value);
        }


        [Fact]
        public async Task TryBindAsync()
        {
            var result = await ErrOr.Of(request3)
                .TryBindAsync<Request, string>(
                    async (x) =>{
                        await Task.Delay(0);
                        return x.Name;
                    }, () => new Exception("null"))
                .TryBindDefaultAsync<string, string>(
                    x => x,
                    async () =>
                    {
                        await Task.Delay(0);
                        return new Exception("null");
                    })
                .TryBindAllAsync<string, string>(
                    async (x) => {
                        await Task.Delay(0);
                        return x;
                    },
                    async () =>
                    {
                        await Task.Delay(0);
                        return new Exception("null");
                    })
                .TryBindAsync<string, string>(x => x, () => new Exception("null"));

            Assert.IsType<Exception>(result.Value);

            var result2 = await ErrOr.Of(request3)
                .TryBindDefaultAsync<Request, string>(
                    x => x.Name,
                    async () =>
                    {
                        await Task.Delay(0);
                        return new Exception("null");
                    });
            Assert.IsType<Exception>(result2.Value);

            var result3 = await ErrOr.Of(request3)
                .TryBindAllAsync<Request, string>(
                    async (x) => {
                        await Task.Delay(0);
                        return x.Name;
                    },
                    async () =>
                    {
                        await Task.Delay(0);
                        return new Exception("null");
                    });
            Assert.IsType<Exception>(result3.Value);
        }

        [Fact]
        public void Tap()
        {
            var result = ErrOr.Of(request3)
                .Tap(x => Console.WriteLine(x.Name))
                .Tap(x => Console.WriteLine(x.Name));

            Assert.Equal(request3, result);
        }

        [Fact]
        public async Task TapAsync()
        {
            var result = await ErrOr.Of(request3)
                .TapAsync(async x =>
                {
                    await Task.Delay(0);
                    Console.WriteLine(x.Name);
                })
                .TapAsync(async x =>
                {
                    await Task.Delay(0);
                    Console.WriteLine(x.Name);
                })
                .TapAsync(x => Console.WriteLine(x.Name));

            Assert.Equal(request3, result);
        }

        private async Task<ErrOr<string>> ToStringResponseAsync(Request request)
        {
            await Task.Delay(1);
            return request.Name;
        }

        private ErrOr<string> ToStringResponse(Request request)
        {
            return request.Name;
        }

        private static async Task<ErrOr<Request>> ValidateNonEmptyAsync(Request request)
        {
            await Task.Delay(1);
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new Exception("Name is empty");
            }
            return request;
        }

        private static ErrOr<Request> ValidateNonEmpty(Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new Exception("Name is empty");
            }
            return request;
        }

        private static ErrOr<Request> ToUpper(Request request) =>
            request with
            {
                Name = request.Name.ToUpperInvariant()
            };

    }
}