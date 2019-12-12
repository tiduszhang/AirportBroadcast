(function () {

    appModule.controller('common.views.logs.reciveLogs.Detail', [
        '$scope', '$uibModal',  '$uibModalInstance',  'content',
        function ($scope, $uibModal,  $uibModalInstance, content) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });
             
            vm.content = content;

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            
        }
    ]);
})();