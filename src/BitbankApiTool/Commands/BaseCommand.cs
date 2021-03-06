﻿using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace BitbankApiTool.Commands
{
    /// <summary>
    /// コマンドの基底クラス
    /// </summary>
    [HelpOption]
    public abstract class BaseCommand
    {
        /// <summary>
        /// service
        /// </summary>
        protected readonly IBitbankApiToolService Service;

        /// <summary>
        /// logger
        /// </summary>
        protected readonly ILogger<BaseCommand> Logger;

        /// <summary>
        /// <see cref="BaseCommand"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        protected BaseCommand(IBitbankApiToolService sampleService, ILogger<BaseCommand> logger)
            => (Service, Logger) = (sampleService, logger);

        /// <summary>
        /// デフォルトの処理
        /// </summary>
        /// <param name="application">コマンドライン</param>
        /// <returns>非同期処理の結果を返します。</returns>
        protected virtual Task OnExecuteAsync(CommandLineApplication application)
        {
            application.ShowHelp();
            return Task.CompletedTask;
        }
    }
}