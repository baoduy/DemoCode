import RootApi from './rootApi';
import Controller from 'restful-action-creator/lib/Controller';

interface PostItem {
  id: number;
  [key: string]: any;
}

/** You can export the original PostApi which provide all Api methods: Refer @type {Controller} for details */
export const PostApi: Controller = RootApi.create('posts');

/** Or Specify all supporting method of POST api. */
export default {
  /** Get All Post if no parameters provided
   * Get by userId parameters is { userId:1 }
   * The URL will be http://jsonplaceholder.typicode.com/posts, the combined of BaseUrl in RootApi and posts name in RootApi.create
   */
  get: (parameters: object | undefined = undefined) =>
    PostApi.get<PostItem>({ params: parameters }),

  /** Get By ID
   * The URL will be http://jsonplaceholder.typicode.com/posts/{id}
   */
  getById: (id: number) => PostApi.get<PostItem>({ pathParams: id }),

  /** Get comments
   * The URL will be http://jsonplaceholder.typicode.com/posts/{id}/comments
   */
  getComments: (id: number) => PostApi.get({ pathParams: [id, 'comments'] }),

  /** Create a new post
   * The URL will be http://jsonplaceholder.typicode.com/posts
   */
  post: (post: PostItem) => PostApi.post({ data: post }),

  /** Update existing post
   * The URL will be http://jsonplaceholder.typicode.com/posts
   */
  put: (post: PostItem) => PostApi.put({ pathParams: post.id, data: post }),

  /** Delete a post
   * The URL will be http://jsonplaceholder.typicode.com/posts/{id}
   */
  delete: (id: number) => PostApi.delete({ pathParams: id })
};
