namespace APWReferenceData
{
    public static class ReferenceDataQueries
    {
        // Static Reference Tables
        #region CountryRelated
        // Country Linked Queries
        public static string GetCountries
        {
            get { return "SELECT ptid, country_code, description, CONCAT(position_1, ',', position_2, ',', position_3, ',', position_4, ',', position_5, ',', position_6, ',', position_7, ',', position_8, ',', position_9, ',', position_10), status FROM ad_gb_country WITH (NOLOCK)"; }
            set { GetCountries = value; }
        }
        public static string GetCities
        {
            get { return "SELECT city_id, city, country_code, status FROM ad_gb_city WITH (NOLOCK)"; }
            set { GetCities = value; }
        }
        public static string GetRegions
        {
            get { return "SELECT ptid, region_id, region, country_code, status FROM ad_gb_region WITH (NOLOCK)"; }
            set { GetRegions = value; }
        }
        public static string GetTerritories
        {
            get { return "SELECT ptid, territory_id, territory, country_code, status FROM ad_gb_territory WITH (NOLOCK)"; }
            set { GetTerritories = value; }
        }
        public static string GetStates
        {
            get { return "SELECT ptid, state, description, country_code, status FROM ad_gb_state WITH (NOLOCK)"; }
            set { GetStates = value; }
        }
        #endregion

        #region AddressRelated
        // Address Related Queries
        public static string GetAddressTypes
        {
            get { return "SELECT ptid, addr_type_id, addr_type, status FROM ad_rm_addr_type  WITH (NOLOCK)"; }
            set { GetAddressTypes = value; }
        }
        #endregion

        #region CustomerRelated
        // Customer Related Queries
        public static string GetTitles
        {
            get { return "SELECT ptid, title_id, title, rim_type, status FROM ad_rm_title  WITH (NOLOCK)"; }
            set { GetTitles = value; }
        }
        public static string GetEducation
        {
            get { return "SELECT ptid, education_id, description, status FROM ad_rm_education  WITH (NOLOCK)"; }
            set { GetEducation = value; }
        }
        public static string GetIdentification
        {
            get { return "SELECT ptid, ident_id, identification, status FROM ad_rm_ident WITH (NOLOCK)"; }
            set { GetIdentification = value; }
        }
        public static string GetSourceOfFunds
        {
            get { return "SELECT ptid, source_funds_id, source_funds, status FROM ad_dp_source_funds WITH (NOLOCK)"; }
            set { GetSourceOfFunds = value; }
        }
        public static string GetCategories
        {
            get { return "SELECT ptid, category_id, description, status FROM ad_rm_category WITH (NOLOCK)"; }
            set { GetCategories = value; }
        }
        public static string GetEmployment
        {
            get { return "SELECT ptid, employment_id, description, type, status FROM ad_rm_employment  WITH (NOLOCK)"; }
            set { GetEmployment = value; }
        }
        public static string GetMaritalTypes
        {
            // This table uses the ad_rm_race table in Phoenix as there is no additional table for Marriage Types
            get { return "SELECT ptid, race_id as marriage_id, race as marriage_type, [status] FROM ad_rm_race  WITH (NOLOCK)"; }
            set { GetMaritalTypes = value; }
        }
        public static string GetMaritalStatuses
        {
            get { return "SELECT id, marital_status, status FROM ad_rm_marital_status WITH (NOLOCK)"; }
            set { GetMaritalStatuses = value; }
        }
        public static string GetRiskTypes
        {
            get { return "SELECT ptid, risk_code, description, status FROM ad_gb_risk WITH (NOLOCK)"; }
            set { GetRiskTypes = value; }
        }
        public static string GetRelationships
        {
            get { return "SELECT rel_id, relationship, 'Rim' as type, status FROM ad_rm_rel WITH (NOLOCK) UNION SELECT rel_id, relationship, 'Account' as type, status FROM ad_gb_acct_rel  WITH (NOLOCK)"; }
            set { GetRelationships = value; }
        }
        public static string GetRIMClasses
        {
            get { return "SELECT ptid, class_code, rim_type, description, rank, ISNULL(country_code,'') as country_code, ISNULL(restrict_id, '') as restrict_id, status FROM ad_rm_cls WITH (NOLOCK)"; }
            set { GetRIMClasses = value; }
        }
        public static string GetMarketingLanguages
        {
            get { return "SELECT ptid, marketing_id, description, status FROM ad_rm_marketing  WITH (NOLOCK)"; }
            set { GetMarketingLanguages = value; }
        }
        public static string GetSICCodes
        {
            get { return "SELECT ptid, sic_code, description, CASE WHEN sic_code < 20000 THEN 'Personal' ELSE 'NonPersonal' END AS rim_type, status FROM ad_gb_sic WITH (NOLOCK) WHERE status = 'Active'"; }
            set { GetSICCodes = value; }
        }
        #endregion

        #region AccountRelated
        // Account Related Queries
        public static string GetAccountPurposes
        {
            get { return "SELECT ptid, purpose_id, purpose, status FROM ad_ln_purpose  WITH (NOLOCK)"; }
            set { GetAccountPurposes = value; }
        }
        public static string GetCurrencies
        {
            get { return "SELECT crncy_id, iso_code, currency_symbol, description, sequence_no, status FROM ad_gb_crncy  WITH (NOLOCK)"; }
            set { GetCurrencies = value; }
        }
        public static string GetApplicationTypes
        {
            get { return "SELECT ptid, appl_type, description, item_type FROM ad_gb_appl_type WITH (NOLOCK)"; }
            set { GetApplicationTypes = value; }
        }
        public static string GetAccountTypes
        {
            get { return "SELECT ptid, acct_type, description, dep_loan, ISNULL(dep_type,'') AS dep_type, appl_type, status FROM ad_gb_acct_type WITH (NOLOCK)"; }
            set { GetAccountTypes = value; }
        }
        public static string GetReasons
        {
            get { return "SELECT ptid, reason_id, description, type, acct_purpose, status FROM ad_gb_reason WITH (NOLOCK)"; }
            set { GetReasons = value; }
        }
        public static string GetCollateralTypes
        {
            get { return "SELECT ptid, collateral_type_id, collateral_type, ISNULL(standard_lien_pos, '') as standard_lien_pos, [status] FROM ad_gb_collateral  WITH (NOLOCK)"; }
            set { GetCollateralTypes = value; }
        }

        public static string GetRates
        {
            get { return "SELECT ptid, index_id, index_name, rate, status FROM ad_gb_rate_index WITH (NOLOCK)"; }
            set { GetRates = value; }
        }
        public static string GetChargeCodes
        {
            get { return "SELECT ptid, charge_code, description, charge_basis, charge_trm, charge_period, charge_type, amt, local_tax, state_tax, module_type, posting_priority, waive,status FROM ad_gb_cc WITH (NOLOCK)"; }
            set { GetChargeCodes = value; }
        }
        public static string GetRoutingNumbers
        {
            get { return "SELECT ptid, routing_no, name, description, city_code, status FROM ad_gb_routing_no WITH (NOLOCK)"; }
            set { GetRoutingNumbers = value; }
        }
        #endregion

        #region StaffRelated
        // Staff Related Queries
        public static string GetRSMClasses
        {
            get { return "SELECT ptid, empl_class_code, [description], [status] FROM ad_gb_rsm_cls  WITH (NOLOCK)"; }
            set { GetRSMClasses = value; }
        }
        public static string GetBranches
        {
            get { return "SELECT ptid, branch_no, routing_no, short_name, name_1, CONCAT(address_line_1, ',', address_line_2, ',', address_line_3, ',', region, ',', territory, ',', country_code) AS branch_address, city, [status] FROM ad_gb_branch  WITH (NOLOCK)"; }
            set { GetBranches = value; }
        }
        public static string GetEmployees
        {
            get { return "SELECT EMPLOYEE_ID, USER_NAME, NAME, SUPERVISOR, BRANCH_NO, EMPL_TYPE, ALLOW_RATE_CHANGE, COUNTRY_CODE, EMPL_CLASS_CODE, RESTRICT_ID, STATUS FROM ad_gb_rsm WITH (NOLOCK)"; }
            set { GetEmployees = value; }
        }
        public static string GetRestrictionLevels
        {
            get { return "SELECT restrict_id, restrict_level, description, status FROM ad_rm_restrict WITH (NOLOCK)"; }
            set { GetRestrictionLevels = value; }
        }
        #endregion

        public static string GetEscrowAgents
        {
            get { return "SELECT a.ptid, a.rim_3rd_esc_id, b.last_name+' - '+c.description as description, a.status FROM rm_3rd_esc AS a WITH (NOLOCK) JOIN rm_acct AS b ON a.rim_no = b.rim_no JOIN ad_rm_3rd_party AS c ON a.rim_3rd_type_id = c.rim_3rd_type_id"; }
            set { GetEscrowAgents = value; }
        }

        public static string GetCycleCodes
        {
            get { return "SELECT ptid, stmt_cycle_code,description,cycle_trm,default_period,cycle_period,status FROM ad_gb_cycle_code WITH (NOLOCK)"; }
            set { GetCycleCodes = value; }
        }



        /*
         *  This section is for the complicated queries required to populate the Deposit and Loan Product Attribute Tables
        */
        public static string GetDPClasses
        {
            get { return "SELECT ptid, class_code, acct_type, description, dep_type, min_open_dep, cc_zero_od, credit_card_product, trm, period, mat_method, crncy_id, status FROM ad_dp_cls WITH (NOLOCK)"; }
            set { GetDPClasses = value; }
        }

        public static string GetDPClassInt
        {
            get { return "SELECT ptid, class_code, acct_type, ISNULL(index_id,0), ISNULL(margin,0), rate_type, ISNULL(int_pmt_method,''), debit_credit FROM ad_dp_cls_int_opt WITH (NOLOCK)"; }
            set { GetDPClassInt = value; }
        }

        public static string GetRIMUserDefinedValues
        {
            get { return "SELECT ptid, class_code, user_defined_id, field_label, low, high, mandatory, default_value, default_value_id, status FROM ad_rm_user_defined WITH (NOLOCK)"; }
            set { GetRIMUserDefinedValues = value; }
        }

        public static string GetUserDefValues
        {
            get { return "SELECT ptid, value_id, user_defined_id, depend_value_id, value FROM ad_gb_user_def_val WITH (NOLOCK)"; }
            set { GetUserDefValues = value; }
        }

        public static string GetODRelatedClassesCounts
        {
            get { return "SELECT class_code, COUNT(acct_type) as od_count FROM gb_cls_cc WITH (NOLOCK) WHERE status = 'Active' AND charge_basis = 'OD Limit' GROUP BY class_code"; }
            set { GetODRelatedClassesCounts = value; }
        }

        public static string GetLNClasses
        {
            get { return "SELECT ptid, class_code, acct_type, description, advance, ISNULL(od_acct, '0'), pmt_method, ISNULL(pmt_type,''), ISNULL(stmt_reqd,'N'), partial_priority, crncy_id, status FROM ad_ln_cls WITH(NOLOCK)"; }
            set { GetLNClasses = value; }
        }

        public static string GetLNClassInt
        {
            get { return "SELECT ptid, class_code, acct_type, ISNULL(index_id,0), ISNULL(cap_int,''), ISNULL(margin,0), ISNULL(margin_type, ''), rate_type, ISNULL([ceiling],0) FROM ad_ln_cls_int_opt WITH (NOLOCK)"; }
            set { GetLNClassInt = value; }
        }

        public static string GetDPClass
        {
            get { return "SELECT ptid, class_code, acct_type, description, 'DP' as type, status FROM ad_dp_cls WITH (NOLOCK)"; }
            set { GetDPClass = value; }
        }

        public static string GetLNClass
        {
            get { return "SELECT class_code, acct_type, description, 'LN' as type, status FROM ad_ln_cls WITH(NOLOCK)"; }
            set { GetLNClass = value; }
        }

        public static string GetDPUserDefined
        {
            get { return "SELECT ptid, class_code, acct_type, user_defined_id, field_label, low, high, mandatory, default_value, default_value_id, 'DP' as type FROM ad_dp_user_defined WITH (NOLOCK)"; }
            set { GetDPUserDefined = value; }
        }

        public static string GetLNUserDefined
        {
            get { return "SELECT ptid, class_code, acct_type, user_defined_id, field_label, low, high, mandatory, default_value, default_value_id, 'LN' as type FROM ad_ln_user_defined WITH (NOLOCK)"; }
            set { GetLNUserDefined = value; }
        }

        public static string GetDPClassRim
        {
            get { return "SELECT ptid, rim_class_code, acct_type, class_code, 'DP' as type, status FROM ad_dp_cls_rim WITH (NOLOCK)"; }
            set { GetDPClassRim = value; }
        }

        public static string GetLNClassRim
        {
            get { return "SELECT ptid, rim_class_code, acct_type, class_code, 'LN' as type, status FROM ad_ln_cls_rim WITH (NOLOCK)"; }
            set { GetLNClassRim = value; }
        }

        public static string GetRMClass
        {
            get { return "SELECT ptid, class_code, description, rim_type, [rank], status FROM ad_rm_cls WITH (NOLOCK)"; }
            set { GetRMClass = value; }
        }
    }
}
