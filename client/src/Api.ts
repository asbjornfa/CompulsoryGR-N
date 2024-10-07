/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface Customer {
  /** @format int32 */
  id?: number;
  name?: string;
  address?: string | null;
  phone?: string | null;
  email?: string | null;
  orders?: Order[];
}

export interface Order {
  /** @format int32 */
  id?: number;
  /** @format date-time */
  orderDate?: string;
  /** @format date */
  deliveryDate?: string | null;
  status?: string;
  /** @format double */
  totalAmount?: number;
  /** @format int32 */
  customerId?: number | null;
  customer?: Customer | null;
  orderEntries?: OrderEntry[];
}

export interface OrderEntry {
  /** @format int32 */
  id?: number;
  /** @format int32 */
  quantity?: number;
  /** @format int32 */
  productId?: number | null;
  /** @format int32 */
  orderId?: number | null;
  order?: Order | null;
  product?: Paper | null;
}

export interface Paper {
  /** @format int32 */
  id?: number;
  name?: string;
  discontinued?: boolean;
  /** @format int32 */
  stock?: number;
  /** @format double */
  price?: number;
  orderEntries?: OrderEntry[];
  properties?: Properties[];
}

export interface Properties {
  /** @format int32 */
  id?: number;
  propertyName?: string;
  papers?: Paper[];
}

export interface RequestCreateCustomerDTO {
  name?: string;
  address?: string;
  phone?: string;
  email?: string;
}

export interface RequestCreateOrderDTO {
  /** @format date-time */
  orderDate?: string;
  /** @format date */
  deliveryDate?: string | null;
  status?: string;
  /** @format double */
  totalAmount?: number;
  /** @format int32 */
  customerId?: number | null;
}

export interface RequestCreateOrderEntryDTO {
  /** @format int32 */
  quantity?: number;
  /** @format int32 */
  productId?: number;
  /** @format int32 */
  orderId?: number;
}

export interface RequestCreatePaperDTO {
  name?: string;
  discontinued?: boolean;
  /** @format int32 */
  stock?: number;
  /** @format double */
  price?: number;
  propertyIds?: number[] | null;
}

export interface RequestCreatePropertyDTO {
  propertyName?: string;
}

import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, HeadersDefaults, ResponseType } from "axios";
import axios from "axios";

export type QueryParamsType = Record<string | number, any>;

export interface FullRequestParams extends Omit<AxiosRequestConfig, "data" | "params" | "url" | "responseType"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType;
  /** request body */
  body?: unknown;
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">;

export interface ApiConfig<SecurityDataType = unknown> extends Omit<AxiosRequestConfig, "data" | "cancelToken"> {
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void;
  secure?: boolean;
  format?: ResponseType;
}

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance;
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private secure?: boolean;
  private format?: ResponseType;

  constructor({ securityWorker, secure, format, ...axiosConfig }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({ ...axiosConfig, baseURL: axiosConfig.baseURL || "http://localhost:5000" });
    this.secure = secure;
    this.format = format;
    this.securityWorker = securityWorker;
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected mergeRequestParams(params1: AxiosRequestConfig, params2?: AxiosRequestConfig): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method);

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method && this.instance.defaults.headers[method.toLowerCase() as keyof HeadersDefaults]) || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === "object" && formItem !== null) {
      return JSON.stringify(formItem);
    } else {
      return `${formItem}`;
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    if (input instanceof FormData) {
      return input;
    }
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key];
      const propertyContent: any[] = property instanceof Array ? property : [property];

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File;
        formData.append(key, isFileType ? formItem : this.stringifyFormItem(formItem));
      }

      return formData;
    }, new FormData());
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<AxiosResponse<T>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const responseFormat = format || this.format || undefined;

    if (type === ContentType.FormData && body && body !== null && typeof body === "object") {
      body = this.createFormData(body as Record<string, unknown>);
    }

    if (type === ContentType.Text && body && body !== null && typeof body !== "string") {
      body = JSON.stringify(body);
    }

    return this.instance.request({
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type ? { "Content-Type": type } : {}),
      },
      params: query,
      responseType: responseFormat,
      data: body,
      url: path,
    });
  };
}

/**
 * @title My Title
 * @version 1.0.0
 * @baseUrl http://localhost:5000
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Customer
     * @name CustomerGetCustomers
     * @request GET:/api/Customer
     */
    customerGetCustomers: (params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Customer`,
        method: "GET",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Customer
     * @name CustomerCreateCustomer
     * @request POST:/api/Customer
     */
    customerCreateCustomer: (data: RequestCreateCustomerDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Customer`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Customer
     * @name CustomerGetCustomerById
     * @request GET:/api/Customer/{id}
     */
    customerGetCustomerById: (id: number, params: RequestParams = {}) =>
      this.request<Customer, any>({
        path: `/api/Customer/${id}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Customer
     * @name CustomerUpdateCustomer
     * @request PUT:/api/Customer/{id}
     */
    customerUpdateCustomer: (id: number, data: RequestCreateCustomerDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Customer/${id}`,
        method: "PUT",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Customer
     * @name CustomerDeleteCustomer
     * @request DELETE:/api/Customer/{id}
     */
    customerDeleteCustomer: (id: number, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Customer/${id}`,
        method: "DELETE",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Order
     * @name OrderGetOrders
     * @request GET:/api/Order
     */
    orderGetOrders: (params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Order`,
        method: "GET",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Order
     * @name OrderCreateOrder
     * @request POST:/api/Order
     */
    orderCreateOrder: (data: RequestCreateOrderDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Order`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Order
     * @name OrderGetOrderById
     * @request GET:/api/Order/{id}
     */
    orderGetOrderById: (id: number, params: RequestParams = {}) =>
      this.request<Order, any>({
        path: `/api/Order/${id}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Order
     * @name OrderUpdateOrder
     * @request PUT:/api/Order/{id}
     */
    orderUpdateOrder: (id: number, data: RequestCreateOrderDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Order/${id}`,
        method: "PUT",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags OrderEntry
     * @name OrderEntryGetOrderEntries
     * @request GET:/api/OrderEntry
     */
    orderEntryGetOrderEntries: (params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/OrderEntry`,
        method: "GET",
        ...params,
      }),

    /**
     * No description
     *
     * @tags OrderEntry
     * @name OrderEntryCreateOrderEntry
     * @request POST:/api/OrderEntry
     */
    orderEntryCreateOrderEntry: (data: RequestCreateOrderEntryDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/OrderEntry`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags OrderEntry
     * @name OrderEntryGetOrderEntryById
     * @request GET:/api/OrderEntry/{id}
     */
    orderEntryGetOrderEntryById: (id: number, params: RequestParams = {}) =>
      this.request<OrderEntry, any>({
        path: `/api/OrderEntry/${id}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Paper
     * @name PaperGetPapers
     * @request GET:/api/Paper
     */
    paperGetPapers: (params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Paper`,
        method: "GET",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Paper
     * @name PaperCreatePaper
     * @request POST:/api/Paper
     */
    paperCreatePaper: (data: RequestCreatePaperDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Paper`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Paper
     * @name PaperGetPaperById
     * @request GET:/api/Paper/{id}
     */
    paperGetPaperById: (id: number, params: RequestParams = {}) =>
      this.request<Paper, any>({
        path: `/api/Paper/${id}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Paper
     * @name PaperUpdatePaper
     * @request PUT:/api/Paper/{id}
     */
    paperUpdatePaper: (id: number, data: RequestCreatePaperDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Paper/${id}`,
        method: "PUT",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Paper
     * @name PaperDeletePaper
     * @request DELETE:/api/Paper/{id}
     */
    paperDeletePaper: (id: number, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Paper/${id}`,
        method: "DELETE",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Properties
     * @name PropertiesGetProperties
     * @request GET:/api/Properties
     */
    propertiesGetProperties: (params: RequestParams = {}) =>
      this.request<Properties[], any>({
        path: `/api/Properties`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Properties
     * @name PropertiesCreateProperty
     * @request POST:/api/Properties
     */
    propertiesCreateProperty: (data: RequestCreatePropertyDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Properties`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Properties
     * @name PropertiesGetPropertiesById
     * @request GET:/api/Properties/{id}
     */
    propertiesGetPropertiesById: (id: number, params: RequestParams = {}) =>
      this.request<Properties, any>({
        path: `/api/Properties/${id}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Properties
     * @name PropertiesUpdateProperty
     * @request PUT:/api/Properties/{id}
     */
    propertiesUpdateProperty: (id: number, data: RequestCreatePropertyDTO, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Properties/${id}`,
        method: "PUT",
        body: data,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Properties
     * @name PropertiesDeleteProperty
     * @request DELETE:/api/Properties/{id}
     */
    propertiesDeleteProperty: (id: number, params: RequestParams = {}) =>
      this.request<File, any>({
        path: `/api/Properties/${id}`,
        method: "DELETE",
        ...params,
      }),
  };
}
