using CMDLogic.EF;
using CMDLogic.Reusable;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CMDLogic.Logic
{
    public class DashboardLogic
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardLogic()
        {
            _dashboardRepository = new DashboardRepository();
        }

        public CommonResponse Add(int byUserID, Dashboard dashboard)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                using (var context = new CMDContext())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            _dashboardRepository.Add(context, byUserID, dashboard);

                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            return commonResponse.Error(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return commonResponse.Error(ex.Message);
            }

            return commonResponse.Success(dashboard);
        }

        public CommonResponse GetAll(int? byUserID = null)
        {
            CommonResponse commonResponse = new CommonResponse();
            IList<Dashboard> result;
            try
            {
                using (var context = new CMDContext())
                {
                    result = _dashboardRepository.GetAll(context);
                }
            }
            catch (Exception ex)
            {
                return commonResponse.Error(ex.Message);
            }

            return commonResponse.Success(result);
        }

        public CommonResponse Remove(int byUserID, Dashboard dashboard)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                using (var context = new CMDContext())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            _dashboardRepository.SetActive(context, byUserID, false, dashboard);

                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            return commonResponse.Error(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return commonResponse.Error(ex.Message);
            }
            return commonResponse.Success(dashboard);            
        }        
    }
}
