(function () {
    appModule.controller('common.views.audioSets.playSet.EditTopPwrPorts', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioPlaySet', 'entityId',
        function ($scope, $uibModalInstance, fileUploader, audioPlaySet, entityId) {
            var vm = this;
          
            vm.saving = false;
            vm.save = function () {
                vm.saving = true;
                var ids = [];
                if (vm.entity) {
                    for (var i = 0; i < vm.entity.length; i++) {
                        if (vm.entity[i].isChoose) {
                            ids.push(vm.entity[i].id);
                        }
                    }

                    var paramters = {
                        dType: entityId.type,
                        id: entityId.entityId,
                        dids: ids
                    };

                    audioPlaySet.updateTopPwrPortList(paramters).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                }
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();

            };


            function init() {
                if (entityId) {
                    audioPlaySet.getTopPwrPortList({
                        dType: entityId.type,
                        id: entityId.entityId
                    }).then(function (result) {
                        vm.entity = result.data; 
                        console.log(vm.entity);
                    });                     
                }

            }

           
            init();
        }
    ]);
})();