(function () {
    appModule.controller('common.views.baseinfo.audioConst.createOrEdit', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioConst',
        'entityId', 'abp.services.app.commonLookup',
        function ($scope, $uibModalInstance, fileUploader, audioConst, entityId, commonLookup) {
            var vm = this;
            vm.languages = [];
            vm.saving = false;
            vm.save = function () {
                vm.saving = true;
                 
                if (entityId) {
                    //修改
                    audioConst.update(vm.entity).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                } else {
                    //创建CreateArtcleCatalog
                    audioConst.create(vm.entity).then(function () {
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
                    audioConst.get({
                        id: entityId
                    }).then(function (result) {
                        vm.entity = result.data;  
                    });                     
                }

                commonLookup.getAudioLanguageForCombobox().then(function (result) {
                    vm.languages = result.data.items;                     
                });

            }
             

            init();
        }
    ]);
})();