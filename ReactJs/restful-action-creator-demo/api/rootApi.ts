import RestfulCreator from 'restful-action-creator';
import adapter from 'axios/lib/adapters/http';

export default RestfulCreator({
  /** The root Url of Web Api */
  baseURL: 'http://jsonplaceholder.typicode.com/',
  /** The Axios adapter: the workaround for unit test */
  adapter: adapter,
  //Custom Authentication: Refer here https://github.com/axios/axios#request-config
  //auth: { username: 'duyhoang', password: '123456' },
  //Global exception handling for all failure request.
  errorHandler: error =>
    Promise.reject({
      error: error.message,
      errorData: error.response && error.response.data
    })
});
