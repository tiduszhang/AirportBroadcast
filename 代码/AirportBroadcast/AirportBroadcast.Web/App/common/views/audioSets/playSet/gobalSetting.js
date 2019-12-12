(function () {
    appModule.controller('common.views.audioSets.playSet.gobalSetting', [
        '$scope', '$uibModalInstance', 'abp.services.app.audioPlaySet', 
        'abp.services.app.tenantSettings',
        function ($scope, $uibModalInstance, audioPlaySet, tenantSettings) {
            var vm = this;

            vm.loading = false;
             
            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.save = function () {   

                vm.loading = true;
                tenantSettings.setAutoPlayGetLuaTimeSpan({ autoPlayGetLuaTimeSpan: vm.autoPlayGetLuaTimeSpan })
                    .then(succ => {                       
                        abp.notify.info("设置成功！");
                        $uibModalInstance.close();
                    }).finally(function () {
                    vm.loading = false;
                });
            };
            vm.init = function () {
                tenantSettings.getAutoPlayGetLuaTimeSpan().then(succ => {
                    console.log(succ.data);
                    vm.autoPlayGetLuaTimeSpan = succ.data.autoPlayGetLuaTimeSpan ;
                });
            }
            vm.init();
         
        }
    ]);
})();