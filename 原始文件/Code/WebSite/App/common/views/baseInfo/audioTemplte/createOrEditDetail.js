(function () {
    appModule.controller('common.views.baseinfo.audioTemplte.createOrEditDetail', [
        '$scope', '$uibModalInstance',  'abp.services.app.audioTemplte',  'entityId',  
        function ($scope, $uibModalInstance,  audioTemplte, entityId) {
            var vm = this; 
            vm.saving = false;
            vm.entity = {
                id: entityId,
                detail: {
                    isParamter: true,
                      sort: 999
                }
            };

            vm.save = function () {
                vm.saving = true;
                  
                audioTemplte.updateDetail(vm.entity).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.saving = false;
                });

            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();

            };
             
        }
    ]);
})();