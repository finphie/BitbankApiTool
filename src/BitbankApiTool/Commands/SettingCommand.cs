using System.Linq;
using System.Threading.Tasks;
using BitbankDotNet;
using BitbankDotNet.Entities;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace BitbankApiTool.Commands
{
    /// <summary>
    /// <see cref="CurrencyPairSetting"/>に関するコマンド
    /// </summary>
    public class SettingCommand : BaseCommand
    {
        /// <summary>
        /// <see cref="SettingCommand"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="sampleService">service</param>
        /// <param name="logger">logger</param>
        public SettingCommand(IBitbankApiToolService sampleService, ILogger<SettingCommand> logger)
            : base(sampleService, logger)
        {
        }

        /// <inheritdoc/>
        protected override async Task OnExecuteAsync(CommandLineApplication application)
        {
            try
            {
                var json = await Service.GetCurrencyPairSettingsAsync().ConfigureAwait(false);
                Logger.LogInformation(string.Join(',', json.Select(x => x.ToString())));
            }
            catch (BitbankDotNetException ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}