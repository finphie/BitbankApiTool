﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace BitbankApiTool.Commands
{
    /// <summary>
    /// <see cref="Transaction"/>に関するコマンド
    /// </summary>
    public class TransactionCommand : BaseCommand
    {
        /// <summary>
        /// 通貨ペア
        /// </summary>
        [Option]
        [Required]
        public CurrencyPair Pair { get; }

        /// <summary>
        /// <see cref="TransactionCommand"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        public TransactionCommand(IBitbankApiToolService sampleService, ILogger<TransactionCommand> logger)
            : base(sampleService, logger)
        {
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(CommandLineApplication application)
        {
            try
            {
                var json = await Service.GetTransactionsAsync(Pair).ConfigureAwait(false);
                Logger.LogInformation(string.Join(',', json.Select(x => x.ToString())));
            }
            catch (BitbankDotNetException ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}
