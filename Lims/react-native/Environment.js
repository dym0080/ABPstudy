const ENV = {
  dev: {
    apiUrl: 'http://localhost:44347',
    oAuthConfig: {
      issuer: 'http://localhost:44347',
      clientId: 'Lims_App',
      clientSecret: '1q2w3e*',
      scope: 'offline_access Lims',
    },
    localization: {
      defaultResourceName: 'Lims',
    },
  },
  prod: {
    apiUrl: 'http://localhost:44347',
    oAuthConfig: {
      issuer: 'http://localhost:44347',
      clientId: 'Lims_App',
      clientSecret: '1q2w3e*',
      scope: 'offline_access Lims',
    },
    localization: {
      defaultResourceName: 'Lims',
    },
  },
};

export const getEnvVars = () => {
  // eslint-disable-next-line no-undef
  return __DEV__ ? ENV.dev : ENV.prod;
};
