(function () {
    //设备管理
    appModule.controller('common.views.play.broadlist.playing', [
        '$scope', '$uibModalInstance', 'abp.services.app.receiveJson', 'entityId',
        function ($scope, $uibModalInstance, receiveJson, entityId) {
            var vm = this;
            vm.loading = false;
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.playlist = [];
            vm.playAudio = {};

            vm.selectAudio = function (entity) {
                vm.playAudio = entity;
            };

            vm.getall = function () {
                vm.loading = true;
                receiveJson.getAllAudioFiles({ id: entityId })
                    .then(function (result) {
                        vm.playlist = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };
            vm.getall();

        }]);
})();