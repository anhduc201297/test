const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  // chainWebpack: (config) => {
  //   config.plugin('html').tap((args) => {
  //     if (args[0].client) {
  //       args[0].client.errors = (error) => !error.message.includes('ResizeObserver');
  //       if (args[0].client.overlay) {
  //         args[0].client.overlay.warnings = false;
  //         args[0].client.overlay.errors = false;
  //       }
  //     }
  //     return args;
  //   });
  // },
  pluginOptions: {
    vuetify: {
			// https://github.com/vuetifyjs/vuetify-loader/tree/next/packages/vuetify-loader
		}
  }
})
