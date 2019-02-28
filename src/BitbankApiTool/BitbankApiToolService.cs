using System;
using System.Net.Http;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BitbankApiTool
{
    /// <summary>
    /// サンプルサービス
    /// </summary>
    public sealed class BitbankApiToolService : IBitbankApiToolService, IDisposable
    {
        static readonly HttpClient Client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        /// <summary>
        /// <see cref="BitbankRestApiClient"/>クラスのインスタンス
        /// </summary>
        readonly BitbankRestApiClient _restApi;

        /// <summary>
        /// 設定
        /// </summary>
        readonly Setting _config;

        /// <summary>
        /// logger
        /// </summary>
        readonly ILogger<BitbankApiToolService> _logger;

        /// <summary>
        /// <see cref="BitbankApiToolService"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="config">設定</param>
        /// <param name="logger">logger</param>
        public BitbankApiToolService(IOptions<Setting> config, ILogger<BitbankApiToolService> logger)
        {
            (_config, _logger) = (config.Value, logger);

            _restApi = new BitbankRestApiClient(Client, _config.Key, _config.Secret);
        }

        /// <inheritdoc/>
        public void Dispose()
            => _restApi.Dispose();

        /// <inheritdoc/>
        public Task<Ticker> GetTickerAsync(CurrencyPair pair)
            => _restApi.GetTickerAsync(pair);

        /// <inheritdoc/>
        public Task<Depth> GetDepthAsync(CurrencyPair pair)
            => _restApi.GetDepthAsync(pair);

        /// <inheritdoc/>
        public Task<Transaction[]> GetTransactionsAsync(CurrencyPair pair)
            => _restApi.GetTransactionsAsync(pair);

        /// <inheritdoc/>
        public Task<Ohlcv[]> GetCandlesticksAsync(CurrencyPair pair, CandleType type, DateTimeOffset dateTime)
            => _restApi.GetCandlesticksAsync(pair, type, dateTime);

        /// <inheritdoc/>
        public Task<Asset[]> GetAssetsAsync()
            => _restApi.GetAssetsAsync();

        /// <inheritdoc/>
        public Task<Order> SendBuyOrderAsync(CurrencyPair pair, decimal amount)
            => _restApi.SendBuyOrderAsync(pair, amount);

        /// <inheritdoc/>
        public Task<Trade[]> GetTradeHistoryAsync(CurrencyPair pair)
            => _restApi.GetTradeHistoryAsync(pair);

        /// <inheritdoc/>
        public Task<WithdrawalAccount[]> GetWithdrawalAccountsAsync(AssetName asset)
            => _restApi.GetWithdrawalAccountsAsync(asset);

        /// <inheritdoc/>
        public Task<HealthStatus[]> GetStatusesAsync()
            => _restApi.GetStatusesAsync();

        /// <inheritdoc/>
        public Task<CurrencyPairSetting[]> GetCurrencyPairSettingsAsync()
            => _restApi.GetCurrencyPairSettingsAsync();
    }
}